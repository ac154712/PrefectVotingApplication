using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrefectVotingApplication.Areas.Identity.Data;

public class AccessDeniedModel : PageModel
{
    private readonly UserManager<PrefectVotingApplicationUser> _userManager;

    public string Message { get; set; }

    public AccessDeniedModel(UserManager<PrefectVotingApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin"))
            {
                Message = $"You're logged in as a {string.Join(", ", roles)}. Only Admins can access this page.";
            }
        }
        else
        {
            Message = "You must be logged in as an Admin to access this page.";
        }

        return Page();
    }
}
