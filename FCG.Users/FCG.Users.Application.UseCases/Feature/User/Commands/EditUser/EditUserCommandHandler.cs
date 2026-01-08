using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public EditUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var objUser = await _userRepository.GetByIdAsync(request.Id);
                objUser.Inicializar(request.Name, request.Email, request.Password, request.UserGroupId);
                await _userRepository.UpdateAsync(objUser);

                return new UserDto() { Id = objUser.Id, Name = objUser.Name, Email = objUser.Email, UserGroupId = objUser.UserGroupId };
            }
            catch (Exception)
            {
                throw new Exception("Ao Editar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
