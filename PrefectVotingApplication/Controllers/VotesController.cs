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
using Microsoft.AspNetCore.Identity;


namespace PrefectVotingApplication.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class VotesController : Controller
    {
        private readonly PrefectVotingApplicationDbContext _context;
        private readonly UserManager<PrefectVotingApplicationUser> _userManager;
        public VotesController(PrefectVotingApplicationDbContext context, UserManager<PrefectVotingApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task LoadUserRoleAsync()//since im using a lot of If statements to check for roles, i made this function to reduce redundancy
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
        // GET: Votes
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder)
        {
            await LoadUserRoleAsync(); // load role before rendering view
            var prefectVotingApplicationDbContext = _context.Votes.Include(v => v.Receiver).Include(v => v.Voter);

          
            var votes = _context.Votes.Include(v => v.Receiver).Include(v => v.Voter).OrderByDescending(v => v.Timestamp); //this sorts by newest date/timestamp


            //pagination
            int pageSize = 16;
            int totalCount = await votes.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            int currentPage = pageNumber ?? 1;

            var pageItems = await votes.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            // pass data to view
            ViewData["PageNumber"] = currentPage;
            ViewData["TotalPages"] = totalPages;

            return View(pageItems);
            //return View(await prefectVotingApplicationDbContext.ToListAsync());
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllVotes(string searchString, int pageNumber = 1)
        {
            await LoadUserRoleAsync(); // load role before rendering view
            int pageSize = 16;

            // gets roles of both voter and receiver for filtering
            var votesQuery = _context.Votes
                .Include(v => v.Voter).ThenInclude(u => u.Role)
                .Include(v => v.Receiver).ThenInclude(u => u.Role).OrderByDescending(v => v.Timestamp).AsQueryable();

            // Only show votes where either the voter OR receiver is Student or Teacher
            votesQuery = votesQuery.Where(v =>
                (v.Voter.Role.RoleName == Role.RoleNames.Student || v.Voter.Role.RoleName == Role.RoleNames.Teacher) &&
                (v.Receiver.Role.RoleName == Role.RoleNames.Student || v.Receiver.Role.RoleName == Role.RoleNames.Teacher));

            if (!string.IsNullOrEmpty(searchString))
            {
                votesQuery = votesQuery.Where(v =>
                    v.Voter.FirstName.Contains(searchString) ||
                    v.Voter.LastName.Contains(searchString) ||
                    v.Receiver.FirstName.Contains(searchString) ||
                    v.Receiver.LastName.Contains(searchString));
            }

            var totalVotes = await votesQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalVotes / (double)pageSize);

            var votes = await votesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentFilter"] = searchString;

            return View(votes);
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
        public async Task<IActionResult> Vote(string receiverId, string viewMode)
        {
            var voterEmail = User.Identity?.Name; //grabs user name(currently logged in)
            var voter = await _context.User.FirstOrDefaultAsync(u => u.Email == voterEmail);

            if (voter == null || string.IsNullOrEmpty(receiverId)) //if a user if not logged-in
            {
                TempData["Message"] = "Vote failed. Login first.";
                return RedirectToAction("Index", "PrefectVotingApplicationUsers", new { viewMode = viewMode ?? "grid" }); 
            }

            var activeElection = await _context.Election // finds the newest election and it has to be ongoing
                .OrderByDescending(e => e.ElectionId)
                .FirstOrDefaultAsync();

            if (activeElection == null) // if there is no active election, then it will show this
            {
                TempData["Message"] = "Wait for Staff to start election.";
                return RedirectToAction("Index", "PrefectVotingApplicationUsers", new { viewMode = viewMode ?? "grid" });
            }

            // finds for duplicate voting for same receiver
            bool alreadyVoted = await _context.Votes
                .AnyAsync(v => v.VoterId == voter.Id && v.ReceiverId == receiverId && v.ElectionId == activeElection.ElectionId);

            if (alreadyVoted) // if user has already voted for the same prefect
            {
                TempData["Message"] = "You have already voted for this prefect.";
                return RedirectToAction("Index", "PrefectVotingApplicationUsers", new { viewMode = viewMode ?? "grid" });
            }

            // Check if user reached 60 vote limit
            int totalUserVotes = await _context.Votes
                .CountAsync(v => v.VoterId == voter.Id && v.ElectionId == activeElection.ElectionId);

            if (totalUserVotes >= 60) //if total votes is over 60 or is 60, it will give an error message instead of counting
            {
                TempData["Message"] = "You already have 60 votes.";
                return RedirectToAction("Index", "PrefectVotingApplicationUsers", new { viewMode = viewMode ?? "grid" });
            }


            var vote = new Votes
            {
                VoterId = voter.Id,
                ReceiverId = receiverId,
                ElectionId = activeElection.ElectionId,
                Timestamp = DateTime.Now
            };

            _context.Votes.Add(vote); 
            await _context.SaveChangesAsync();

            TempData["Message"] = "Vote successfully recorded!"; //if the if statements are passed or not gotten captured, then the vote is successful
            return RedirectToAction("Index", "PrefectVotingApplicationUsers", new { viewMode = viewMode ?? "grid" });
        }
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

        public async Task<IActionResult> YourVotes(string searchString, string sortOrder, int pageNumber = 1, string viewMode = "grid")
        {
            await LoadUserRoleAsync(); // load role before rendering view
            int pageSize = 16;

            // get logged in user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            // fetch votes made by this user
            var votesQuery = _context.Votes.Include(v => v.Receiver)
                .Where(v => v.VoterId == currentUser.Id)
                .AsQueryable();

            // search
            if (!string.IsNullOrEmpty(searchString))
            {
                votesQuery = votesQuery.Where(v =>
                    v.Receiver.FirstName.Contains(searchString) || v.Receiver.LastName.Contains(searchString));
            }

            // sort
            ViewData["FirstNameSortParm"] = sortOrder == "first_desc" ? "first_asc" : "first_desc";
            ViewData["LastNameSortParm"] = sortOrder == "last_desc" ? "last_asc" : "last_desc";

            votesQuery = sortOrder switch
            {
                "first_asc" => votesQuery.OrderBy(v => v.Receiver.FirstName),
                "first_desc" => votesQuery.OrderByDescending(v => v.Receiver.FirstName),
                "last_asc" => votesQuery.OrderBy(v => v.Receiver.LastName),
                "last_desc" => votesQuery.OrderByDescending(v => v.Receiver.LastName),
                _ => votesQuery.OrderByDescending(v => v.Timestamp)
            };

            // pagination
            var totalCount = await votesQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var votes = await votesQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentFilter"] = searchString;
            ViewData["ViewMode"] = viewMode;

            return View(votes);
        }
        public async Task<IActionResult> TopStudents(string viewMode = "grid")
        {
            await LoadUserRoleAsync();
            // looks for current/active election 
            var activeElection = await _context.Election
                .FirstOrDefaultAsync(e => e.Status == Election.ElectionStatus.Active);

            if (activeElection == null) 
            {
                // No election -> return empty list
                ViewBag.Message = "There is currently no active election.";
                ViewData["ViewMode"] = viewMode;
                return View(new List<object>());
            }

            var studentRole = await _context.Role // gets RoleId for student 
                .FirstOrDefaultAsync(r => r.RoleName == Role.RoleNames.Student);

            if (studentRole == null)
            {
                ViewData["ViewMode"] = viewMode;
                return View(new List<object>());
            }

            var studentRoleId = studentRole.RoleId;

            // Query top 60 student candidates by total weighted votes
            var topStudents = await _context.Votes
                .Where(v => v.ElectionId == activeElection.ElectionId
                    && v.Receiver.RoleId == studentRoleId) // filter by RoleId (more reliable)
                .Include(v => v.Voter) // we only need voter.RoleId (no deep nav required)
                .ThenInclude(voter => voter.Role)
                .Include(v => v.Receiver)
                .GroupBy(v => new
                {
                    v.Receiver.Id,
                    v.Receiver.FirstName,
                    v.Receiver.LastName,
                    v.Receiver.Description,
                    v.Receiver.ImagePath
                })
                .Select(g => new
                {
                    g.Key.Id,
                    g.Key.FirstName,
                    g.Key.LastName,
                    g.Key.Description,
                    g.Key.ImagePath,
                    TotalVotes = g.Sum(v => (int?)(v.Voter.Role != null ? v.Voter.Role.VoteWeight : 0)) ?? 0
                })
                .OrderByDescending(x => x.TotalVotes)
                .Take(60)
                .ToListAsync();

            ViewData["ViewMode"] = viewMode;
            return View("~/Views/Votes/TopStudents.cshtml", topStudents);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveVote(int id) // Delete method but simpler(no confirmation)
        {
            var vote = await _context.Votes.FindAsync(id); //finds id
            if (vote != null && vote.VoterId == _userManager.GetUserId(User))
            {
                _context.Votes.Remove(vote); // deletes id
                await _context.SaveChangesAsync();
                TempData["Success"] = "Vote removed successfully!";
            }
            return RedirectToAction(nameof(YourVotes));
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
