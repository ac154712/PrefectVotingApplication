using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;

namespace PrefectVotingApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VotesController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;

        public VotesController(PrefectVotingApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Votes
        public async Task<IActionResult> Index()
        {
            var prefectVotingApplicationDbContext = _context.Votes.Include(v => v.Receiver).Include(v => v.Voter);
            return View(await prefectVotingApplicationDbContext.ToListAsync());
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votes = await _context.Votes
                .Include(v => v.Receiver)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (votes == null)
            {
                return NotFound();
            }

            return View(votes);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id");
            ViewData["VoterId"] = new SelectList(_context.User, "Id", "Id");
            ViewBag.ElectionId = new SelectList(_context.Election, "ElectionId", "ElectionTitle");
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoteId,VoterId,ReceiverId,ElectionId,Timestamp")] Votes votes)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(votes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id", votes.ReceiverId);
            ViewData["VoterId"] = new SelectList(_context.User, "Id", "Id", votes.VoterId);
            ViewBag.ElectionId = new SelectList(_context.Election, "ElectionId", "ElectionTitle");
            return View(votes);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votes = await _context.Votes.FindAsync(id);
            if (votes == null)
            {
                return NotFound();
            }
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id", votes.ReceiverId);
            ViewData["VoterId"] = new SelectList(_context.User, "Id", "Id", votes.VoterId);
            ViewBag.ElectionId = new SelectList(_context.Election, "ElectionId", "ElectionTitle");
            return View(votes);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoteId,VoterId,ReceiverId,ElectionId,Timestamp")] Votes votes)
        {
            if (id != votes.VoteId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(votes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotesExists(votes.VoteId))
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
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id", votes.ReceiverId);
            ViewData["VoterId"] = new SelectList(_context.User, "Id", "Id", votes.VoterId);
            ViewBag.ElectionId = new SelectList(_context.Election, "ElectionId", "ElectionTitle");
            return View(votes);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votes = await _context.Votes
                .Include(v => v.Receiver)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (votes == null)
            {
                return NotFound();
            }

            return View(votes);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var votes = await _context.Votes.FindAsync(id);
            if (votes != null)
            {
                _context.Votes.Remove(votes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotesExists(int id)
        {
            return _context.Votes.Any(e => e.VoteId == id);
        }
    }
}
