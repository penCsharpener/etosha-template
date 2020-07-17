using System.Threading.Tasks;
using Template.Domain.Models;

namespace Template.Server.Providers.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<User> Login(LoginModel model);
    }
}
