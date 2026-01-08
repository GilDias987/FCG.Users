using FCG.Users.Application.Dto.UserGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetUserGroup
{
    public class GetUserGroupQuery : IRequest<UserGroupDto>
    {
        public int Id { get; set; }
    }
}
