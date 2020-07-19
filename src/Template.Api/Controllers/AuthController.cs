using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.Api.Infrastructure.Security;
using Template.Domain.Models;
using Template.Server.Providers.Interfaces;

namespace Template.Api.Controllers
{
    [Route("api/[controller]")]
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

            var user = await _authenticationProvider.LoginAsync(model);

            if (user == null)
            {
                _logger.LogError($"User not found for email: ${model?.Email} and password with length ${model?.Password?.Length ?? 0}");
                return BadRequest();
            }

            var token = _webTokenBuilder.GenerateToken(user);
            return Ok(new { user.FirstName, user.LastName, Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!model.Password.Equals(model.PasswordConfirm))
            {
                return BadRequest("Password confirmation doesn't match.");
            }

            var result = await _authenticationProvider.RegisterAsync(model, (obj) => Url.Action(nameof(VerifyEmail), "Auth", obj, Request.Scheme, Request.Host.ToString()));

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("verifyemail")]
        public async Task<IActionResult> VerifyEmail(string username, string code)
        {
            var result = await _authenticationProvider.VerifyEmail(username, code);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
