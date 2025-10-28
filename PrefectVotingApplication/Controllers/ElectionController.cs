using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;
using static PrefectVotingApplication.Models.Election;

namespace PrefectVotingApplication.Controllers
{
    //[Authorize(Roles = "Admin")] 
    public class ElectionController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;
        private readonly UserManager<PrefectVotingApplicationUser> _userManager;

        public ElectionController(PrefectVotingApplicationDbContext context, UserManager<PrefectVotingApplicationUser> userManager)
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
        private async Task EnsureSingleActiveElectionAsync(int? keepActiveElectionId = null) //only one active election at a time
        {
            var otherElections = await _context.Election
                .Where(e => e.Status == Election.ElectionStatus.Active &&
                            (keepActiveElectionId == null || e.ElectionId != keepActiveElectionId))
                .ToListAsync();

            foreach (var e in otherElections)
            {
                e.Status = Election.ElectionStatus.Pending;
                _context.Update(e);
            }

            await _context.SaveChangesAsync();
        }

        // GET: Election
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            await LoadUserRoleAsync(); // load role before rendering view
            var elections = _context.Election.AsQueryable();

            // --- SEARCH ---
            if (!string.IsNullOrEmpty(searchString))
            {
                elections = elections.Where(e => e.ElectionTitle.Contains(searchString));
            }

            // --- SORT BY NEWEST (default) ---
            elections = elections.OrderByDescending(e => e.StartDate);

            // --- PAGINATION ---
            int pageSize = 10;
            int currentPage = pageNumber ?? 1;
            int totalCount = await elections.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageItems = await elections
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // --- PASS TO VIEW ---
            ViewData["PageNumber"] = currentPage;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentFilter"] = searchString;

            return View(pageItems);
        }

        // GET: Election/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Election
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // GET: Election/Create
        public IActionResult Create()
        {
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ElectionStatus)));

            return View();
        }

        // POST: Election/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectionId,ElectionTitle,StartDate,EndDate,Status")] Election election)
        {
            
            if (!ModelState.IsValid)
            {
                election.Status = Election.ElectionStatus.Pending; // makes all new created elections pending by default

                _context.Add(election);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ElectionStatus))); don't need this code no more because i will delete viewbag in razor page
            return View(election);
        }

        // GET: Election/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Election.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ElectionStatus)));
            return View(election);
        }

        // POST: Election/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectionId,ElectionTitle,StartDate,EndDate,Status")] Election election)
        {
            if (id != election.ElectionId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // --- Check if another election is already active ---
                if (election.Status == Election.ElectionStatus.Active)
                {
                    var existingActive = await _context.Election
                        .FirstOrDefaultAsync(e => e.Status == Election.ElectionStatus.Active && e.ElectionId != election.ElectionId);

                    if (existingActive != null)
                    {
                        // Error message to user
                        ModelState.AddModelError("", "There is already an active election. Please make the pending it first before activating a new one.");

                        ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(Election.ElectionStatus)));
                        return View(election);
                    }
                }


                try
                {
                    _context.Update(election);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Election.Any(e => e.ElectionId == election.ElectionId))/*(!ElectionExists(election.ElectionId))*/
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
            return View(election);
        }

        // GET: Election/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Election
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // POST: Election/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var election = await _context.Election.FindAsync(id);
            if (election != null)
            {
                _context.Election.Remove(election);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectionExists(int id)
        {
            return _context.Election.Any(e => e.ElectionId == id);
        }
    }
}
