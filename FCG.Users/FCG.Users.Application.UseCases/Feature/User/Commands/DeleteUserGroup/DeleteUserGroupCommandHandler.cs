using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUserGroup
{
    public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, bool>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<bool> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
        {
            var repUserGroup = await _userGroupRepository.GetByIdAsync(request.Id);
            if (repUserGroup != null)
            {
                await _userGroupRepository.DeleteAsync(repUserGroup.Id);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Usuário não foi encontrado.");
            }
        }
    }
}
