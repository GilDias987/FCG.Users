using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.UseCases.Feature.User.Commands.AddUserSeed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUserSeed
{
    public class AddUserSeedCommandHandler : IRequestHandler<AddUserSeedCommand,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;

        public AddUserSeedCommandHandler(IUserRepository userRepository, IUserGroupRepository userGroupRepository)
        {
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
        }

        public async Task<bool> Handle(AddUserSeedCommand request, CancellationToken cancellationToken)
        {
            var userGroups = await _userGroupRepository.ListUserGroupAsync();
          
            if (!userGroups.Any())
            {
                var userGrupoAdmin = await _userGroupRepository.AddAsync(new Domain.Entities.UserGroup("ADMINISTRADOR"));
                var userGrupoUser = await _userGroupRepository.AddAsync(new Domain.Entities.UserGroup("USUARIO"));

                var userAdmin = await _userRepository.AddAsync(new Domain.Entities.User("Admin", "admin@gmail.com", "Admin@987", userGrupoAdmin.Id));
            }

            return true;
        }
    }
}
