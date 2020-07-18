using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common;
using Template.Persistence.Entities;

namespace Template.Persistence
{
    internal sealed class DbInitializer
    {
        internal static async Task Seed(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<AppDbContext>();
            var roleManager = provider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();

            if (!context.Users.Any())
            {
                await roleManager.CreateAsync(new AppRole(Constants.AdministratorRoleName));
                await roleManager.CreateAsync(new AppRole(Constants.UserRoleName));

                var user = AppUser.SeedNewUser("admin", "The", "Administrator", "admin@admin.com", "a");
                user.Id = 1;
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                await userManager.AddToRoleAsync(user, Constants.AdministratorRoleName);

                var user2 = AppUser.SeedNewUser("sam", "Sam", "Sample", "sam@sample.com", "a");
                user2.Id = 2;
                await context.Users.AddAsync(user2);
                await context.SaveChangesAsync();
                await userManager.AddToRoleAsync(user2, Constants.UserRoleName);
            }
        }
    }
}
