using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MassTransit;
using MediatR;


namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IEmailService _emailService;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "users:all";

        public AddUserCommandHandler(IUserRepository userRepository, IUserGroupRepository userGroupRepository, ISendEndpointProvider sendEndpointProvider, ISendEndpointProvider send, IEmailService emailService, ICacheService cacheService)
        {
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
            _sendEndpointProvider = sendEndpointProvider;
            _emailService = emailService;
            _cacheService = cacheService;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objUser = await _userRepository.AddAsync(new Domain.Entities.User(request.Name, request.Email, request.Password, request.UserGroupId));
                var objUserGroup = await _userGroupRepository.GetByIdAsync(request.UserGroupId);

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                var user = new UserDto
                {
                    Id          = objUser.Id,
                    Name        = objUser.Name,
                    Email       = objUser.Email,
                    UserGroupId = objUser.UserGroupId,
                    Group       = objUserGroup.Name
                };

                var endpoint = await _sendEndpointProvider
                    .GetSendEndpoint(new Uri("queue:email-queue"));

                var email = _emailService.EmailMessage(user.Email, user.Name);
                await endpoint.Send(email);

                return user;
         
            }
            catch (Exception ex)
            {
                throw new Exception("Ao Adicionar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
