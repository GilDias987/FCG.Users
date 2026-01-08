using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.LoginUser
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        private readonly IUserRepository _userRepository;
        public LoginUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Informe o e-mail");

            RuleFor(x => x.Email)
               .EmailAddress()
               .When(x => !string.IsNullOrEmpty(x.Email))
               .WithMessage("E-mail inválido.");

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("Informe o senha.");

        }
    }
}
