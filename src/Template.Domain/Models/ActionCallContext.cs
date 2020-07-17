using System;
using System.Security.Claims;

namespace Template.Domain.Models
{
    public class ActionCallContext
    {
        public ActionCallContext(ClaimsPrincipal principal)
        {
            Principal = principal;
            UserId = Convert.ToInt32(principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            Username = principal.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            Email = principal.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public ClaimsPrincipal Principal { get; }

        public int UserId { get; set; }

        public string Email { get; }

        public string Username { get; }
    }
}
