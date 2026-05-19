using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "users:all";

        public EditUserCommandHandler(IUserRepository userRepository, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
        }

        public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objUser = await _userRepository.GetByIdAsync(request.Id);
                objUser.Initialize(request.Name, request.Email, request.Password, request.UserGroupId);
                await _userRepository.UpdateAsync(objUser);

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return new UserDto() 
                { 
                    Id          = objUser.Id, 
                    Name        = objUser.Name, 
                    Email       = objUser.Email, 
                    UserGroupId = objUser.UserGroupId 
                };
            }
            catch (Exception)
            {
                throw new Exception("Ao Editar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
