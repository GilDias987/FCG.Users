using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUserGroup
{
    public sealed class DeleteUserGroupValidator : AbstractValidator<DeleteUserGroupCommand>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        public DeleteUserGroupValidator(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;

            RuleFor(x => x.Id)
                 .NotEmpty()
                 .WithMessage("Informe o id do usuário.")
                 .GreaterThan(0)
                 .WithMessage("O id deve ser maior que zero.")
                 .MustAsync(async (Id, cancellation) => (await _userGroupRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
                 .WithMessage("O id grupo de usuario informado não foi encontrado.");
        }
    }
}
