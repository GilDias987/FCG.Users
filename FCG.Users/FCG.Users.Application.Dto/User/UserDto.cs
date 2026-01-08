using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.Dto.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserGroupId { get; set; }
        public string Group { get; set; }
    }
}
