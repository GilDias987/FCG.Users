using FCG.Shared.Contracts;
using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MassTransit;
using MediatR;


namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public AddUserCommandHandler(IUserRepository userRepository, IUserGroupRepository userGroupRepository, ISendEndpointProvider sendEndpointProvider, ISendEndpointProvider send)
        {
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objUser = await _userRepository.AddAsync(new Domain.Entities.User(request.Name, request.Email, request.Password, request.UserGroupId));
                var objUserGroup = await _userGroupRepository.GetByIdAsync(request.UserGroupId);

                var user = new UserDto
                {
                    Id = objUser.Id,
                    Name = objUser.Name,
                    Email = objUser.Email,
                    UserGroupId = objUser.UserGroupId,
                    Group = objUserGroup.Name
                };

                var endpoint = await _sendEndpointProvider
                    .GetSendEndpoint(new Uri("queue:user-create-queue"));

                await endpoint.Send(new UserCreatedEvent { Email = user.Email, Name = user.Name });

                return user;
         
            }
            catch (Exception ex)
            {
                throw new Exception("Ao Adicionar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
