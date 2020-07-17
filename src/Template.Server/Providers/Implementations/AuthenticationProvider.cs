using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Common.Validation;
using Template.Domain.Models;
using Template.Persistence.Entities;
using Template.Server.Providers.Interfaces;

namespace Template.Server.Providers.Implementations
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Login(LoginModel model)
        {
            Require.ThatNotNull(model, nameof(model));

            var dbUser = await _userManager.FindByEmailAsync(model.Email);
            if (dbUser == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(dbUser, model.Password))
            {
                return new User(dbUser.Id, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.UserName);
            }

            return null;
        }
    }
}
