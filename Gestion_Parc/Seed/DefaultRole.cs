using System;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Parc.Seed
{
    public static class DefaultRole
    {
        public static async Task CreateRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SERVICE INFORMATIQUE"));
            }

        }
    }
}
