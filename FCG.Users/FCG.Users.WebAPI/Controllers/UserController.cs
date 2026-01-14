using FCG.Users.Application.UseCases.Feature.User.Commands.AddUser;
using FCG.Users.Application.UseCases.Feature.UserGroup.Commands.AddUserGroup;
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
    //[Authorize(Policy = "ADMINISTRADOR")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Incluir Usuário
        /// </summary>
        /// <param name="addUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertUser(AddUserCommand addUserCommand)
        {
            var user = await _mediator.Send(addUserCommand);

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
            var isDeleted = await _mediator.Send(new DeleteUserCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Usuário deletado com sucesso");
            }

            return NotFound();
        }

        /// <summary>
        /// Obter Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
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
            var usuario = await _mediator.Send(new GetAllUserQuery());

            return Ok(usuario);
        }
    }
}
