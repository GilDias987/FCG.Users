using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.Dto.Authentication
{
    public class LoginUserDto
    {
        public required string Email { get; set; }

        public required string Senha { get; set; }
    }
}
