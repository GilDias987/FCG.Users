using FCG.Users.Application.UseCases.Feature.User.Queries.GetUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.AddUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.DeleteUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.EditUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Queries.GetAllUserGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Users.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class UserGroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserGroupController> _logger;

        public UserGroupController(IMediator mediator, ILogger<UserGroupController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Incluir
        /// </summary>
        /// <param name="AddUserGroupCommand"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertGroupUser([FromBody] AddUserGroupCommand addUserGroupCommand)
        {
            _logger.LogInformation("Iniciando criação de grupo de usuário: {GroupName}", addUserGroupCommand.Name);

            var userGroup = await _mediator.Send(addUserGroupCommand);

            _logger.LogInformation("Grupo criado com sucesso. ID: {GroupId}", userGroup.Id);

            return CreatedAtAction("InsertGroupUser", userGroup);
        }

        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="editGrupoUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateGroupUser([FromBody] EditUserGroupCommand editUserGroupCommand)
        {
            _logger.LogInformation("Atualizando grupo ID: {GroupId}", editUserGroupCommand.Id);

            var userGroup = await _mediator.Send(editUserGroupCommand);
            return Ok(userGroup);
        }

        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> DeletarUserGroup(int id)
        {
            _logger.LogWarning("Tentativa de exclusão do grupo ID: {GroupId}", id);

            var isDeleted = await _mediator.Send(new DeleteUserGroupCommand { Id = id });
            if (isDeleted)
            {
                _logger.LogInformation("Grupo {GroupId} deletado com sucesso.", id);

                return Ok("Grupo de Usuario foi deletado com sucesso.");
            }

            _logger.LogWarning("Falha ao deletar: Grupo {GroupId} não encontrado.", id);

            return NotFound();
        }

        /// <summary>
        /// Obter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get{id}")]
        public async Task<IActionResult> GetGrupoUsuario(int id)
        {
            _logger.LogDebug("Buscando grupo ID: {GroupId}", id);

            var userGroup = await _mediator.Send(new GetUserGroupQuery { Id = id });

            return Ok(userGroup);
        }

        /// <summary>
        /// Obter todos grupos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllGrupoUsuario()
        {
            _logger.LogInformation("Listando todos os grupos de usuários.");

            var userGroup = await _mediator.Send(new GetAllUserGroupQuery());

            return Ok(userGroup);
        }
    }
}
