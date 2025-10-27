using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;

namespace PrefectVotingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<PrefectVotingApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<PrefectVotingApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.Users
                                             .Include(u => u.Role) // include your custom role
                                             .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                ViewBag.RoleName = user?.Role?.RoleName.ToString(); // store as string
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
