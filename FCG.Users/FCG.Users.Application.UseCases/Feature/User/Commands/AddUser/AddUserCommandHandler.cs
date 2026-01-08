using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository, IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objUser = await _userRepository.AddAsync(new Domain.Entities.User(request.Name, request.Email, request.Password, request.UserGroupId));
                var objUserGroup = await _userGroupRepository.GetByIdAsync(request.UserGroupId);
                return new UserDto() { Id = objUser.Id, 
                                       Name = objUser.Name, 
                                       Email = objUser.Email, 
                                       UserGroupId = objUser.UserGroupId,
                                       Group = objUserGroup.Name
                };
            }
            catch (Exception)
            {
                throw new Exception("Ao Adicionar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
