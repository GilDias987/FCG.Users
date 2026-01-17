using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.Dto.User
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
