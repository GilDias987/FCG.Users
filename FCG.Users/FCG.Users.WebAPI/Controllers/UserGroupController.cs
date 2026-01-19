using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.DeleteUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Queries.GetAllUserGroup;
using FCG.Users.Application.UseCases.Feature.User.Queries.GetUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.AddUserGroup;
using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.EditUserGroup;
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
        public UserGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Incluir
        /// </summary>
        /// <param name="AddUserGroupCommand"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertGroupUser([FromBody] AddUserGroupCommand addUserGroupCommand)
        {
            var userGroup = await _mediator.Send(addUserGroupCommand);
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
            var isDeleted = await _mediator.Send(new DeleteUserGroupCommand { Id = id });

            if (isDeleted)
            {
                return Ok("Grupo de Usuario foi deletado com sucesso.");
            }

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
            var userGroup = await _mediator.Send(new GetAllUserGroupQuery());

            return Ok(userGroup);
        }
    }
}
