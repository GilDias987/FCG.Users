using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.UseCases.Feature.User.Queries.GetUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetUserGroup
{
    public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, UserGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroupDto> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
        {

            var groupUser = await _userGroupRepository.GetByIdAsync(request.Id);

            if (groupUser is null)
            {
                throw new ArgumentException("Grupo de usuário não encontrado.");
            }

            return new UserGroupDto { Id = groupUser.Id, Name = groupUser.Name };
        }
    }
}
