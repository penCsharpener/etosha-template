using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.Domain.Actions;
using Template.Server.Execution;

namespace Template.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RolesController : BaseApiController
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IActionExecutor _actionExecutor;

        public RolesController(ClaimsPrincipal claimsPrincipal, ILogger<RolesController> logger, IActionExecutor executor) : base(claimsPrincipal)
        {
            _logger = logger;
            _actionExecutor = executor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var action = new ListRoleAction(_actionCallContext);
            var result = await _actionExecutor.Execute(action);

            return Ok(result.Roles);
        }
    }
}
