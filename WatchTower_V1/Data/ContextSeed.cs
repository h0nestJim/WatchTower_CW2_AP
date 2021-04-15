using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTower_V1.Models;

namespace WatchTower_V1.Data
{
    public enum Roles
    {
        Admin,
        Manager,
        Support,
        User
    }
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Support.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new UserModel
            {
                UserName = "AdminWT",
                Email = "superadmin@gmail.com",
                Fname = "Admin",
                SName = "User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Adm!n123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Support.ToString());
                }

            }
        }

        internal static Task SeedSuperAdminAsync(object userManager, object roleManager)
        {
            throw new NotImplementedException();
        }
    }


}
