using PrefectVotingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PrefectVotingApplication.Areas.Identity.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(PrefectVotingApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<PrefectVotingApplicationUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager, IConfiguration configuration)
        {
            context.Database.EnsureCreated();

            // seeding models with default data or data the database needs before it is created, bsically integral part where it should be created before ideally

            if (!context.Role.Any()) //only seeds if database is empty
            {
                var roles = new List<Role>
                {
                new Role {RoleName = Role.RoleNames.Student, VoteWeight = 1},
                new Role {RoleName = Role.RoleNames.Teacher, VoteWeight = 20},
                new Role {RoleName = Role.RoleNames.Staff, VoteWeight = 0} //school staff


                };
                context.Role.AddRange(roles);
                context.SaveChanges();
            }

            //seeds admin role on identity aspnetroles table, not my own custom role model
            if (!await roleManager.RoleExistsAsync("Admin")) //checks if there is already admin
            {
                await roleManager.CreateAsync(new IdentityRole("Admin")); //if no, it will add one (usually when this app has just started)
            }


            var adminEmail = configuration["AdminUser:Email"]; //this gets values from the config file(appsettingsjson)
            var adminPassword = configuration["AdminUser:Password"];

            if (await userManager.FindByEmailAsync(adminEmail) == null) //this checks if there is an admin user that already exists by email
            {
                var adminUser = new PrefectVotingApplicationUser //if not then it adds an admin user to prefectvotingapplication user model using the credentials hidden
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    ImagePath = "",
                    Description = "",
                    FirstName = "PVA",
                    LastName = "Admin",
                    RoleId = context.Role.First(r => r.RoleName == Role.RoleNames.Staff).RoleId
                };//we need this because we won't be able to login in the website without registering this beforehand before the application even starts

                var result = await userManager.CreateAsync(adminUser, adminPassword);// this creates the user in the identity
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin"); //this specific line assigns the identity role of this user to admin
                }
            }
        }
    }
}