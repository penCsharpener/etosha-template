using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Actions;
using Template.Domain.Models;
using Template.Persistence;
using Template.Server.ActionHandlers.Base;

namespace Template.Server.ActionHandlers.RoleActionHandlers
{
    internal class ListRoleActionHandler : AbstractActionHandler<ListRoleAction, ListRoleActionResult>
    {
        private readonly AppDbContext _context;

        public ListRoleActionHandler(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        protected override async Task<ListRoleActionResult> ExecuteInternal(ListRoleAction action)
        {
            var roles = await _context.Roles.Select(r => new UserRole(r.Id, r.Name)).ToArrayAsync();

            return new ListRoleActionResult(action, roles);
        }
    }
}
