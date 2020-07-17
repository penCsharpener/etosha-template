using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.Api.Infrastructure.Security;
using Template.Domain.Models;
using Template.Server.Providers.Interfaces;

namespace Template.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IWebTokenBuilder _webTokenBuilder;
        private readonly IAuthenticationProvider _authenticationProvider;

        public AuthController(ILogger<AuthController> logger, IWebTokenBuilder webTokenBuilder, IAuthenticationProvider authenticationProvider)
        {
            _logger = logger;
            _webTokenBuilder = webTokenBuilder;
            _authenticationProvider = authenticationProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model for user: ${model?.Email} and password with length ${model?.Password?.Length ?? 0}");
                return BadRequest(ModelState);
            }

            var user = await _authenticationProvider.Login(model);

            if (user == null)
            {
                _logger.LogError($"User not found for email: ${model?.Email} and password with length ${model?.Password?.Length ?? 0}");
                return BadRequest();
            }

            var token = _webTokenBuilder.GenerateToken(user);
            return Ok(new { user.FirstName, user.LastName, Token = token });
        }
    }
}
