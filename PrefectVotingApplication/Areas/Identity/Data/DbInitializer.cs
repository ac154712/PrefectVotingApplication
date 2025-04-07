using PrefectVotingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PrefectVotingApplication.Areas.Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PrefectVotingApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // seeding models with default data or data the database needs before it is created, bsically integral part where it should be created before ideally

            if (!context.Role.Any()) //only seeds if database is empty
            {
                var roles = new List<Role>
                {
                new Role {RoleName = Role.RoleNames.Student, VoteWeight = 1},
                new Role {RoleName = Role.RoleNames.Teacher, VoteWeight = 20},
                new Role {RoleName = Role.RoleNames.Admin, VoteWeight = 0}


                };
                context.Role.AddRange(roles);
                context.SaveChanges();
            }
        }
    }
}