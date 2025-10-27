using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;

namespace PrefectVotingApplication.Controllers

{
    
    [Route("Users")]// makes the route Users instead of whatever it is whenever this controller is referenced in the many files in my project
    public class PrefectVotingApplicationUsersController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;
        private readonly UserManager<PrefectVotingApplicationUser> _userManager;

        public PrefectVotingApplicationUsersController(PrefectVotingApplicationDbContext context, UserManager<PrefectVotingApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task LoadUserRoleAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var fullUser = await _context.Users
                        .Include(u => u.Role)
                        .FirstOrDefaultAsync(u => u.Id == user.Id);

                    ViewBag.RoleName = fullUser?.Role?.RoleName.ToString();
                }
            }
        }

        // GET: PrefectVotingApplicationUsers
        [HttpGet("")]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, string viewMode = "grid")
        {
            await LoadUserRoleAsync(); // load role before rendering view
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            var prefectVotingApplicationDbContext = _context.User.Include(p => p.Role);
            ViewData["CurrentFilter"] = searchString;
            ViewData["ViewMode"] = viewMode; 
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;



            //var users = _context.User.Include(u => u.Role).AsQueryable();
            var currentUserId = _userManager.GetUserId(User);

            //users = users.Where(u => u.Role.RoleName == Role.RoleNames.Student); // Filter to only students

            var users = _context.User
            .Include(u => u.Role)
            .Where(u => u.Role.RoleName == Role.RoleNames.Student && u.Id != currentUserId); //Filters to only students and removes current loggined in suer
            


            // search
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u =>
                    u.FirstName.Contains(searchString) ||
                    u.LastName.Contains(searchString) ||
                    u.Email.Contains(searchString));
            }

            // sortn logiv
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
                case "LastName":
                    users = users.OrderBy(u => u.LastName);
                    break;
                case "lastname_desc":
                    users = users.OrderByDescending(u => u.LastName);
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName);
                    break;
            }

            //pagination
            int pageSize = 16;
            int pageIndex = pageNumber ?? 1;
            int totalItems = await users.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedUsers = await users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewData["PageNumber"] = pageIndex;
            ViewData["TotalPages"] = totalPages;

            return View(pagedUsers);
            return View(await prefectVotingApplicationDbContext.ToListAsync());
        }

        // GET: PrefectVotingApplicationUsers/Details/5
        [HttpGet("PrefectVotingApplicationUsers/Details/5")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prefectVotingApplicationUsers = await _context.User
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prefectVotingApplicationUsers == null)
            {
                return NotFound();
            }

            return View(prefectVotingApplicationUsers);
        }

        // GET: PrefectVotingApplicationUsers/Create
        [HttpGet("PrefectVotingApplicationUsers/Create")]
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleId");
            return View();
        }

        // POST: PrefectVotingApplicationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("PrefectVotingApplicationUsers/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,ImagePath,Description,RoleId")] PrefectVotingApplicationUser prefectVotingApplicationUsers)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(prefectVotingApplicationUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleId", prefectVotingApplicationUsers.RoleId);
            return View(prefectVotingApplicationUsers);
        }

        // GET: PrefectVotingApplicationUsers/Edit/5
        [HttpGet("PrefectVotingApplicationUsers/Edit/5")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prefectVotingApplicationUsers = await _context.User.FindAsync(id);
            if (prefectVotingApplicationUsers == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleId", prefectVotingApplicationUsers.RoleId);
            return View(prefectVotingApplicationUsers);
        }

        // POST: PrefectVotingApplicationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("PrefectVotingApplicationUsers/Edit/5")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,ImagePath,Description,RoleId")] PrefectVotingApplicationUser prefectVotingApplicationUsers)
        {
            if (id != prefectVotingApplicationUsers.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(prefectVotingApplicationUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrefectVotingApplicationUsersExists(prefectVotingApplicationUsers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleId", prefectVotingApplicationUsers.RoleId);
            return View(prefectVotingApplicationUsers);
        }

        // GET: PrefectVotingApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prefectVotingApplicationUsers = await _context.User
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prefectVotingApplicationUsers == null)
            {
                return NotFound();
            }

            return View(prefectVotingApplicationUsers);
        }

        // POST: PrefectVotingApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var prefectVotingApplicationUsers = await _context.User.FindAsync(id);
            if (prefectVotingApplicationUsers != null)
            {
                _context.User.Remove(prefectVotingApplicationUsers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrefectVotingApplicationUsersExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
