using System;
using System.Threading.Tasks;
using Template.Domain.Models;

namespace Template.Server.Providers.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<User> LoginAsync(LoginModel model);
        Task<bool> RegisterAsync(RegisterModel model, Func<object, string> urlAction);
        Task<bool> VerifyEmail(string userId, string code);
    }
}
