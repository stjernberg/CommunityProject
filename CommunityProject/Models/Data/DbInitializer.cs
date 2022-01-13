using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Data
{
    public class DbInitializer
    {
        internal static async Task InitializeAsync(CommunityDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            //If there's no role
            if (!context.Roles.Any())
            {
                //Create SuperAdmin role

                IdentityRole role = new IdentityRole("SuperAdmin");
                IdentityResult result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    ErrorMessages(result);
                }

                //Create user and add to SuperAdmin role
                AppUser appUser = new AppUser
                {
                    FirstName = "Super",
                    LastName = "SuperAdminSon",
                    UserName = "SuperAdmin",
                    Email = "superadmin@app.com",
                    PhoneNr = "13423"
                   
                };

                IdentityResult userResult = await userManager.CreateAsync(appUser, "Super183?");

                if (!userResult.Succeeded)
                {
                    ErrorMessages(userResult);
                }

                userManager.AddToRoleAsync(appUser, role.Name).Wait();
            }

            //If there is no admin role
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                //Create Admin role
                IdentityRole adminRole = new IdentityRole("Admin");
                IdentityResult adminResult = await roleManager.CreateAsync(adminRole);

                if (!adminResult.Succeeded)
                {
                    ErrorMessages(adminResult);
                }

                //Create user and add to Admin role
                AppUser appUser = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "AdminSon",
                    UserName = "Admin",
                    Email = "admin@app.com",
                    PhoneNr = "1674423"

                };

                IdentityResult identityResult = await userManager.CreateAsync(appUser, "Admin352?");

                if (!identityResult.Succeeded)
                {
                    ErrorMessages(identityResult);
                }

                userManager.AddToRoleAsync(appUser, adminRole.Name).Wait();
            }
        }

        private static void ErrorMessages(IdentityResult identityResult)
        {
            string errors = "";
            foreach (var error in identityResult.Errors)
            {
                errors += error.Code + ",  " + error.Description;
            }
            throw new Exception(errors);
        }

    }
}
