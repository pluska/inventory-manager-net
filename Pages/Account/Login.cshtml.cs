using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManager.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public bool RememberMe { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public void OnGet()
    {
        _logger.LogInformation("Login page GET request");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("Login attempt for user {Email}", Email);
        
        try
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Email and password are required.";
                _logger.LogWarning("Login attempt with missing email or password");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} logged in successfully", Email);
                // Redirect to the dashboard
                return Redirect("/dashboard");
            }
            else
            {
                _logger.LogWarning("Failed login attempt for user {Email}", Email);
                ErrorMessage = "Invalid login attempt. Please check your email and password.";
                return Page();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {Email}", Email);
            ErrorMessage = "An error occurred during login. Please try again.";
            return Page();
        }
    }
}
