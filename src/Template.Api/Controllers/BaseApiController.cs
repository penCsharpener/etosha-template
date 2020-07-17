using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Template.Domain.Models;

namespace Template.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected ActionCallContext _actionCallContext;

        public BaseApiController(ClaimsPrincipal claimsPrincipal)
        {
            _actionCallContext = new ActionCallContext(claimsPrincipal);
        }
    }
}
