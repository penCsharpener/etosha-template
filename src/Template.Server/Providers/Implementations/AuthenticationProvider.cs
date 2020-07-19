using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NETCore.MailKit.Core;
using Template.Domain.Common.Validation;
using Template.Domain.Models;
using Template.Persistence.Entities;
using Template.Server.Providers.Interfaces;

namespace Template.Server.Providers.Implementations
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public AuthenticationProvider(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<User> LoginAsync(LoginModel model)
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

        public async Task<bool> RegisterAsync(RegisterModel model, Func<object, string> urlAction)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                UserName = model.Username,
                NormalizedUserName = model.Username.ToUpper(),
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var link = urlAction.Invoke(new { userName = user.UserName, code });

                await _emailService.SendAsync(user.Email, "Register Confirmation", $"<a href=\"{link}\">Verify Email<a/>", true);
            }

            return result.Succeeded;
        }

        public async Task<bool> VerifyEmail(string username, string code)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded;
        }
    }
}
