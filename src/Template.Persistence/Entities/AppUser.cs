using System;
using Microsoft.AspNetCore.Identity;

namespace Template.Persistence.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser() : this(null) { }

        public AppUser(string userName) : this(userName, null, null, null) { }

        public AppUser(string userName, string firstName, string lastName, string email) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static AppUser SeedNewUser(string username, string firstName, string lastName, string email, string password)
        {
            var appUser = new AppUser()
            {
                Email = email,
                UserName = username,
                NormalizedUserName = email.ToUpper(),
                NormalizedEmail = email.ToUpper(),
                FirstName = firstName,
                LastName = lastName,
                LockoutEnabled = false,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            appUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(appUser, password);

            return appUser;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
