using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManager.Pages.Account;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public void OnGet()
    {
        _logger.LogInformation("Register page GET request");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("Registration attempt for user {Email}", Email);
        
        try
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ErrorMessage = "All fields are required.";
                _logger.LogWarning("Registration attempt with missing fields");
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Password and confirmation password do not match.";
                _logger.LogWarning("Registration attempt with password mismatch");
                return Page();
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long.";
                _logger.LogWarning("Registration attempt with short password");
                return Page();
            }

            var user = new IdentityUser { UserName = Email, Email = Email };
            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} registered successfully", Email);

                // Sign in the user after successful registration
                await _signInManager.SignInAsync(user, isPersistent: false);
                
                return Redirect("/dashboard");
            }

            foreach (var error in result.Errors)
            {
                ErrorMessage += error.Description + " ";
            }
            
            _logger.LogWarning("Registration failed for user {Email}: {Errors}", Email, ErrorMessage);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for user {Email}", Email);
            ErrorMessage = "An error occurred during registration. Please try again.";
            return Page();
        }
    }
}
