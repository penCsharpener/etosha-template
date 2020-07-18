using Template.Domain.Actions.Base;
using Template.Domain.Models;

namespace Template.Domain.Actions
{
    public class ListRoleAction : AbstractAction<ListRoleActionResult>
    {
        public ListRoleAction(ActionCallContext context) : base(context) { }
    }

    public class ListRoleActionResult : AbstractActionResult<ListRoleAction>
    {
        public ListRoleActionResult(ListRoleAction action, UserRole[] roles) : base(action)
        {
            Roles = roles;
        }

        public UserRole[] Roles { get; }
    }
}
