using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.EditUserGroup
{
    public sealed class EditUserGroupValidator : AbstractValidator<EditUserGroupCommand>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        public EditUserGroupValidator(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;

            RuleFor(x => x.Id)
              .MustAsync(async (Id, cancellation) => (await _userGroupRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id informado não foi encontrado.");

            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage("Informe o nome do grupo.")
              .MustAsync(async (model, context, cancellationToken) =>
              {
                  var grupousuario = await _userGroupRepository.GetByIdAsync(model.Id);

                  if (grupousuario != null && grupousuario.Name != model.Name)
                  {
                      var verificaGrupoUsuario = await _userGroupRepository.VerifyExistsGroupAsync(model.Name);
                      return !verificaGrupoUsuario;
                  }

                  return true;
              })
              .WithMessage("Já existe um grupo de usuário com esse nome.");

        }
    }
}
