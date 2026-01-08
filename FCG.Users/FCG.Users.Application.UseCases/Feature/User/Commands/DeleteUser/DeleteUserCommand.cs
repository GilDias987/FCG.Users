using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUser
{

    public class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
