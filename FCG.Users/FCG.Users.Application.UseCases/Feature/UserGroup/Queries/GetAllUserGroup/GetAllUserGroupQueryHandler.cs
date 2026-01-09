using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Queries.GetAllUserGroup
{

    public class GetAllUserGroupQueryHandler : IRequestHandler<GetAllUserGroupQuery, List<UserGroupDto>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetAllUserGroupQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<List<UserGroupDto>> Handle(GetAllUserGroupQuery request, CancellationToken cancellationToken)
        {
            var qryUserGroup = _userGroupRepository.All;

            var lstUserGroup = qryUserGroup.ToList().Select(x=> new UserGroupDto {
                Id = x.Id,
                Name = x.Name
            });

            if (!lstUserGroup.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return lstUserGroup.ToList();
        }
    }
}
