using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Domain.Common.Exception
{
    public class DomainException : System.Exception
    {
        public DomainException(string message) : base(message)
        {

        }

        public static void When(bool hasError, string errorMessage)
        {
            if (hasError)
                throw new DomainException(errorMessage);
        }
    }
}
