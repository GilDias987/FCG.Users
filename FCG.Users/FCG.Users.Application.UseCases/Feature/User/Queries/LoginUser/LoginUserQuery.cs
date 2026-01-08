using FCG.Users.Application.Dto.User;
using MediatR;
using System;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
