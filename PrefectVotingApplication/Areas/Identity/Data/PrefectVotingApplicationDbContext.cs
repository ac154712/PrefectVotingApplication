using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;

namespace PrefectVotingApplication.Areas.Identity.Data;

public class PrefectVotingApplicationDbContext : IdentityDbContext<PrefectVotingApplicationUser>
{
    public PrefectVotingApplicationDbContext(DbContextOptions<PrefectVotingApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<PrefectVotingApplication.Areas.Identity.Data.PrefectVotingApplicationUser> User { get; set; } = default!;

public DbSet<PrefectVotingApplication.Models.Role> Role { get; set; } = default!;

public DbSet<PrefectVotingApplication.Models.Election> Election { get; set; } = default!;

public DbSet<PrefectVotingApplication.Models.Votes> Votes { get; set; } = default!;

public DbSet<PrefectVotingApplication.Models.AuditLog> AuditLog { get; set; } = default!;
}
