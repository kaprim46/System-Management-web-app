using System;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Parc.Seed
{
	public static class DefaultUser
	{
        public static async Task CreateAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                Email = "kaprim@gmail.com",
                FullName = "Rima Krizi",
                UserName = "kaprim@gmail.com",
                ImageUser = "11726b69-8758-4c07-be0d-4c92c07464ac.jpeg",
                DepartmentName = "HR",
                EmailConfirmed = true,
                ActiveUser = true,
                PhoneNumberConfirmed = true
            };
            var user = userManager.FindByEmailAsync(defaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(defaultUser, "123456");
                //to assign many roles
                await userManager.AddToRolesAsync(defaultUser, new List<string> { "Admin" });
                // to assign one role => await userManager.AddToRoleAsync(defaultUser, "SuperAdmin");
            }
        }

    }
}

