using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;

namespace PrefectVotingApplication.Controllers
{
    [Route("AuditLog")] // makes the route AuditLog instead of AuditLogs whenever this controller is referenced in the many files in my project
    public class AuditLogsController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;

        public AuditLogsController(PrefectVotingApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuditLogs
        [HttpGet("")] //this just gets the route i've set
        public async Task<IActionResult> Index()
        {
            var prefectVotingApplicationDbContext = _context.AuditLog.Include(a => a.User).Include(a => a.Vote);
            return View(await prefectVotingApplicationDbContext.ToListAsync());
        }

        // GET: AuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLog
                .Include(a => a.User)
                .Include(a => a.Vote)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return View(auditLog);
        }

        // GET: AuditLogs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email");
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "VoteId");
            return View();
        }

        // POST: AuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,VoteId,UserId,Action,Timestamp")] AuditLog auditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", auditLog.UserId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "VoteId", auditLog.VoteId);
            return View(auditLog);
        }

        // GET: AuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLog.FindAsync(id);
            if (auditLog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", auditLog.UserId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "VoteId", auditLog.VoteId);
            return View(auditLog);
        }

        // POST: AuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,VoteId,UserId,Action,Timestamp")] AuditLog auditLog)
        {
            if (id != auditLog.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditLogExists(auditLog.LogId))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", auditLog.UserId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "VoteId", auditLog.VoteId);
            return View(auditLog);
        }

        // GET: AuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLog
                .Include(a => a.User)
                .Include(a => a.Vote)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return View(auditLog);
        }

        // POST: AuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditLog = await _context.AuditLog.FindAsync(id);
            if (auditLog != null)
            {
                _context.AuditLog.Remove(auditLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditLogExists(int id)
        {
            return _context.AuditLog.Any(e => e.LogId == id);
        }
    }
}
