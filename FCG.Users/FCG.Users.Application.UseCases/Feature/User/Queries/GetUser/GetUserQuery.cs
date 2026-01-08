using FCG.Users.Application.Dto.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetUser
{

    public class GetUserQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
