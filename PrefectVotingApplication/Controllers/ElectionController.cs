using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;
using static PrefectVotingApplication.Models.Election;

namespace PrefectVotingApplication.Controllers
{
    [Authorize(Roles = "Staff")]
    [Authorize(Roles = "Admin")]
    public class ElectionController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;

        public ElectionController(PrefectVotingApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Election
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
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
                _context.Add(election);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ElectionStatus)));
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
                try
                {
                    _context.Update(election);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectionExists(election.ElectionId))
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
