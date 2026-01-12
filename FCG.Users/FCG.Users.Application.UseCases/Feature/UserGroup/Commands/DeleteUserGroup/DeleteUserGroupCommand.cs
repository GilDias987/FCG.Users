using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.DeleteUserGroup
{
    public class DeleteUserGroupCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
