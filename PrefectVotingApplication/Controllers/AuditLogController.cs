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
    
    public class AuditLogController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;

        public AuditLogController(PrefectVotingApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuditLog
        public async Task<IActionResult> Index()
        {
            var prefectVotingApplicationDbContext = _context.AuditLog.Include(a => a.User).Include(a => a.Vote);
            return View(await prefectVotingApplicationDbContext.ToListAsync());
        }

        // GET: AuditLog/Details/5
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

        // GET: AuditLog/Create
        public IActionResult Create()
        {
            ViewBag.ActionList = new SelectList(Enum.GetValues(typeof(AuditLog.AuditAction)));
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "ReceiverId");
            return View();
        }

        // POST: AuditLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,VoteId,UserId,Action,Timestamp")] AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(auditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", auditLog.UserId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "ReceiverId", auditLog.VoteId);
            ViewBag.ActionList = new SelectList(Enum.GetValues(typeof(AuditLog.AuditAction)));
            return View(auditLog);
        }

        // GET: AuditLog/Edit/5
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", auditLog.UserId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "ReceiverId", auditLog.VoteId);
            ViewBag.ActionList = new SelectList(Enum.GetValues(typeof(AuditLog.AuditAction)));
            return View(auditLog);
        }

        // POST: AuditLog/Edit/5
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

            if (!ModelState.IsValid)
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", auditLog.UserId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "VoteId", "ReceiverId", auditLog.VoteId);
            ViewBag.ActionList = new SelectList(Enum.GetValues(typeof(AuditLog.AuditAction)));
            return View(auditLog);
        }

        // GET: AuditLog/Delete/5
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

        // POST: AuditLog/Delete/5
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
