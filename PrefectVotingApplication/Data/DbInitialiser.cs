using PrefectVotingApplication.Areas.Identity.Data;
using PrefectVotingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AvcolGrpsCharity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PrefectVotingApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // seeding models with default data like ROLES,
            
            if (!context.Roles.Any()) //only seeds if database is empty
            {
                var roles = new List<Role>
                {
                    new Role {RoleId = 1, RoleName = Role.RoleNames.Student, VoteWeight = 1},
                    new Role {RoleId = 2, RoleName = Role.RoleNames.Teacher, VoteWeight = 20},
                    new Role {RoleId = 3, RoleName = Role.RoleNames.Admin, VoteWeight = 0}
                };
            }

            

            context.SaveChanges();
        }
    }
}