using FCG.Users.Application.Dto.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
    }
}
