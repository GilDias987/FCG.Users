using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.EditUser
{
    public sealed class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;

        public EditUserValidator(IUserRepository userRepository, IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;

            RuleFor(x => x.Id)
              .MustAsync(async (Id, cancellation) => (await _userRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do usuário informado não foi encontrado.");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Informe o nome.");

            RuleFor(c => c.Password)
                 .NotEmpty()
                 .WithMessage("Informe a senha.")
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

            RuleFor(c => c.UserGroupId).NotEmpty().WithMessage("Informe o grupo do usuário.");

            RuleFor(x => x.UserGroupId)
              .MustAsync(async (UserGroupId, cancellation) => (await _userGroupRepository.GetByIdAsync(UserGroupId)) != null ? true : false)
              .WithMessage("O id de grupo de usuário não foi encontrado.");

            RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Informe um e-mail válido.")
            .MustAsync(async (model, context, cancellationToken) =>
            {
                var user = await _userRepository.GetByIdAsync(model.Id);

                if (user != null && user.Email != model.Email)
                {
                    var verifyUser = await _userRepository.VerifyUserEmailAsync(model.Email);
                    return !verifyUser;
                }

                return true;
            })
            .WithMessage("Este e-mail já está registrado. Por favor, tente outro.");
        }
    }
}
