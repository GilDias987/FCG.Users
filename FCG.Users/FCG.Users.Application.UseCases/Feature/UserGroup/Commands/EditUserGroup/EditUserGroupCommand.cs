using FCG.Users.Application.Dto.UserGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.EditUserGroup
{
    public class EditUserGroupCommand : IRequest<UserGroupDto>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
