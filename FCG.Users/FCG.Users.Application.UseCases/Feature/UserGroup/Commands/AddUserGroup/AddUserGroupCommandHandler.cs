using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.AddUserGroup
{
    public class AddUserGroupCommandHandler : IRequestHandler<AddUserGroupCommand, UserGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public AddUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroupDto> Handle(AddUserGroupCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var objUserGroup = await _userGroupRepository.AddAsync(new Domain.Entities.UserGroup(request.Name));

                return new UserGroupDto() { Id = objUserGroup.Id, Name = objUserGroup.Name };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o Grupo de usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
