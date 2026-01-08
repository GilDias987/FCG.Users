using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUserGroup
{
    public sealed class AddUserGroupValidator : AbstractValidator<AddUserGroupCommand>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        public AddUserGroupValidator(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;

            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage("Informe o nome do grupo.")
              .MustAsync(async (Nome, cancellation) => string.IsNullOrEmpty(Nome) ? true : !(await _userGroupRepository.VerifyExistsGroupAsync(Nome)))
              .WithMessage("Já existe um grupo de usuário com esse nome.");

        }
    }
}
