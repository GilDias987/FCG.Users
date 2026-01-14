using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public AddUserCommandHandler(IUserRepository userRepository, IUserGroupRepository userGroupRepository, IBus bus)
        {
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var objUser = await _userRepository.AddAsync(new Domain.Entities.User(request.Name, request.Email, request.Password, request.UserGroupId));
                //var objUserGroup = await _userGroupRepository.GetByIdAsync(request.UserGroupId);

                var user = new UserDto
                {
                    Id = 1,
                    Name = "Gil Dias",
                    Email = "gil@fiap.com.br",
                    UserGroupId = 2,
                    Group = "Administrador"
                };

                // Envia a mensagem para o RabbitMQ
                await _bus.Publish(user);

                return user;
         
            }
            catch (Exception)
            {
                throw new Exception("Ao Adicionar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
