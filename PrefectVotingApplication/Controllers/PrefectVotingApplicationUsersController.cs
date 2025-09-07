using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;

namespace PrefectVotingApplication.Controllers

{
    
    [Route("Users")]// makes the route Users instead of whatever it is whenever this controller is referenced in the many files in my project
    public class PrefectVotingApplicationUsersController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;

        public PrefectVotingApplicationUsersController(PrefectVotingApplicationDbContext context)
        {
            _context = context;
        }

 
        // GET: PrefectVotingApplicationUsers
        [HttpGet("")]
        public async Task<IActionResult> Index(string viewMode = "grid")
        {
            var prefectVotingApplicationDbContext = _context.User.Include(p => p.Role);
            ViewData["ViewMode"] = viewMode; // pass current mode
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
