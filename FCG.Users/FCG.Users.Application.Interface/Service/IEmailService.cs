using FCG.Users.Application.Dto.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.Interface.Service
{
    public interface IEmailService
    {
        public EmailMessageDto EmailMessage(string email, string name);
    }
}
