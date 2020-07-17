using Template.Domain.Models;

namespace Template.Api.Infrastructure.Security
{
    public interface IWebTokenBuilder
    {
        string GenerateToken(User user);
    }
}
