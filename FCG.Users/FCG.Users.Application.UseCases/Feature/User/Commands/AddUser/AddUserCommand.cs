using FCG.Users.Application.Dto.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{
    public class AddUserCommand : IRequest<UserDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserGroupId { get; set; }
    }
}
