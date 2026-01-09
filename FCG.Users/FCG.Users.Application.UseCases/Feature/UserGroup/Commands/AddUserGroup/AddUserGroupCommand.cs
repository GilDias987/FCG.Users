using FCG.Users.Application.Dto.UserGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.AddUserGroup
{
    public class AddUserGroupCommand : IRequest<UserGroupDto>
    {
        public required string Name { get; set; }
    }
}
