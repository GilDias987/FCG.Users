using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{
    public sealed class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserRepository _userRepository;

        public AddUserValidator(IUserGroupRepository userGroupRepository, IUserRepository userRepository)
        {
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;

            RuleFor(c => c.Name).NotEmpty().WithMessage("Informe o nome.");

            RuleFor(c => c.Password)
               .Must((model, context) =>
               {
                   if (model.Password.Length < 8)
                   {
                       return false;
                   }
                   if (!Regex.IsMatch(model.Password, "[a-zA-Z]"))
                   {
                       return false;
                   }
                   if (!Regex.IsMatch(model.Password, "[0-9]"))
                   {
                       return false;
                   }
                   if (!Regex.IsMatch(model.Password, "[0-9]"))
                   {
                       return false;
                   }

                   return true;
               })
               .WithMessage("A senha deve ter no mínimo 8 caracteres e incluir pelo menos uma letra maiúscula, um número e um caractere especial.");


            RuleFor(x => x.UserGroupId)
              .MustAsync(async (UsuarioGrupoId, cancellation) => (await _userGroupRepository.GetByIdAsync(UsuarioGrupoId)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id de grupo de usuário não foi encontrado.");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Informe um e-mail válido.")
                .MustAsync(async (Email, cancellation) => !(await _userRepository.VerifyUserEmailAsync(Email)))
                .WithMessage("Este e-mail já está registrado. Por favor, tente outro.")
                .Must((model, context) =>
                {
                    if (!Regex.IsMatch(model.Email, @"@(gmail\.com|fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"))
                    {
                        return false;
                    }

                    return true;
                })
                   .WithMessage("E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.");
        }
    }
}
