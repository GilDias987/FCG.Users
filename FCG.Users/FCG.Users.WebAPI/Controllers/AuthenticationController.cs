using FCG.Users.Application.UseCases.Feature.User.Queries.LoginUser;
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
        private readonly ILogger<AuthenticationController> _logger; // Adicionado

        public AuthenticationController(IMediator mediator, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _mediator = mediator;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserRequest)
        {
            _logger.LogInformation("Tentativa de login iniciada para o usuário: {UserName}", loginUserRequest.Email);

            try
            {
                var user = await _mediator.Send(loginUserRequest);

                AuthenticationToken authenticationToken = new AuthenticationToken(_configuration);
                var token = authenticationToken.GenerateToken(user);

                _logger.LogInformation("Login realizado com sucesso para: {UserName}", loginUserRequest.Email);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar login para o usuário: {UserName}", loginUserRequest.Email);

                return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
            }
        }
    }
}
