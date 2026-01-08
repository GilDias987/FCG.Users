using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetUserGroup
{
    public sealed class GetUserGroupValidator
   : AbstractValidator<GetUserGroupQuery>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        public GetUserGroupValidator(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;

            RuleFor(x => x.Id)
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _userGroupRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id informado não foi encontrado.");

        }
    }
}
