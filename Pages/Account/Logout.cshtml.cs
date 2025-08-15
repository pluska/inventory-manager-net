using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManager.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LogoutModel> _logger;

    public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            
            // Redirect to login page after logout
            return Redirect("/login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
            // In case of error, redirect to login anyway
            return Redirect("/login");
        }
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out via POST");
            
            // Redirect to login page after logout
            return Redirect("/login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout via POST");
            // In case of error, redirect to login anyway
            return Redirect("/login");
        }
    }
}
