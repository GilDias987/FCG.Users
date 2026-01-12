using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Queries.GetAllUserGroup
{
    public class GetAllUserGroupQuery : IRequest<List<UserGroupDto>>
    {
    }
}
