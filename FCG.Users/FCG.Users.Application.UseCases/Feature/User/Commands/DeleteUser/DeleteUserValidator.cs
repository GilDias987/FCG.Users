using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUser
{
    public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Informe o id do usuário.")
            .GreaterThan(0)
            .WithMessage("O id deve ser maior que zero.")
            .MustAsync(async (Id, cancellation) => (await _userRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
            .WithMessage("O id do usuario informado não foi encontrado.");
        }
    }
}
