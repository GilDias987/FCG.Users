using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.EditUserGroup
{
    public class EditUserGroupCommandHandler : IRequestHandler<EditUserGroupCommand, UserGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public EditUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroupDto> Handle(EditUserGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _userGroupRepository.GetByIdAsync(request.Id);
            group.Inicializar(request.Name);
            await _userGroupRepository.UpdateAsync(group);
            return new UserGroupDto() { Id = group.Id, Name = group.Name };
        }
    }

}
