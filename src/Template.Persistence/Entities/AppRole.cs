using Microsoft.AspNetCore.Identity;

namespace Template.Persistence.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() { }

        public AppRole(string roleName) : base(roleName) { }
    }
}
