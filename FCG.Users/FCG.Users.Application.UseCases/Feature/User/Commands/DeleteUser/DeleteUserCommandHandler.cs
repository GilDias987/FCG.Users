using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "users:all";

        public DeleteUserCommandHandler(IUserRepository userRepository, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var repUser = await _userRepository.GetByIdAsync(request.Id);
            if (repUser != null)
            {
                await _userRepository.DeleteAsync(repUser.Id);

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Usuário não foi encontrado.");
            }
        }
    }
}
