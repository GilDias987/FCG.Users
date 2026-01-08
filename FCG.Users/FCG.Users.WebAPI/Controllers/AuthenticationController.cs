using FCG.Users.Application.UseCases.Feature.User.Queries.LoginUser;
// Depenências
using FCG.Users.WebAPI.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Users.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public AuthenticationController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserRequest)
        {
            try
            {
                var user = await _mediator.Send(loginUserRequest);
                AuthenticationToken authenticationToken = new AuthenticationToken(_configuration);
                var token = authenticationToken.GenerateToken(user);
                return Ok(new { token });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
