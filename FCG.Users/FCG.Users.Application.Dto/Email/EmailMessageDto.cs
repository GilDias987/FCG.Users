using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.Dto.Email
{
    public class EmailMessageDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
