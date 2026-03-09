using FCG.Users.Application.UseCases.Feature.User.Commands.AddUser;
using FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUser;
using FCG.Users.Application.UseCases.Feature.User.Commands.EditUser;
using FCG.Users.Application.UseCases.Feature.User.Queries.GetAllUser;
using FCG.Users.Application.UseCases.Feature.User.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Users.WebAPI.Controllers
{
    /// <summary>
    /// Usuário
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Incluir Usuário
        /// </summary>
        /// <param name="addUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertUser(AddUserCommand addUserCommand)
        {
            _logger.LogInformation("Iniciando inclusão de novo usuário: {@UserCommand}", addUserCommand);

            var user = await _mediator.Send(addUserCommand);

            _logger.LogInformation("Usuário {UserId} incluído com sucesso.", user.Id);

            return Created($"/api/user/{user.Id}", user);
        }

        /// <summary>
        /// Alterar Usuário
        /// </summary>
        /// <param name="editUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserCommand editUserCommand)
        {
            _logger.LogInformation("Solicitação de alteração para o usuário ID: {UserId}", editUserCommand.Id);

            var usuario = await _mediator.Send(editUserCommand);

            return Ok(usuario);
        }

        /// <summary>
        /// Deletar Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogWarning("Tentativa de exclusão do usuário ID: {UserId}", id);

            var isDeleted = await _mediator.Send(new DeleteUserCommand { Id = id });
            if (isDeleted)
            {
                _logger.LogInformation("Usuário {UserId} deletado com sucesso.", id);

                return Ok("Usuário deletado com sucesso");
            }

            _logger.LogWarning("Falha ao deletar: Usuário {UserId} não encontrado.", id);

            return NotFound();
        }

        /// <summary>
        /// Obter Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogDebug("Buscando detalhes do usuário ID: {UserId}", id);

            var user = await _mediator.Send(new GetUserQuery { Id = id });

            return Ok(user);
        }

        /// <summary>
        /// Obter todos os Usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsuario()
        {
            _logger.LogInformation("Listando todos os usuários.");

            var usuario = await _mediator.Send(new GetAllUserQuery());

            return Ok(usuario);
        }
    }
}
