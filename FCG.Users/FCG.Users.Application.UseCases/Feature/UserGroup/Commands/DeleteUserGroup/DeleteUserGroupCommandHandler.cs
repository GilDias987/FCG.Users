using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.DeleteUserGroup
{
    public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, bool>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "user-groups:all";

        public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository, ICacheService cacheService)
        {
            _userGroupRepository = userGroupRepository;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
        {
            var repUserGroup = await _userGroupRepository.GetByIdAsync(request.Id);
            if (repUserGroup != null)
            {
                await _userGroupRepository.DeleteAsync(repUserGroup.Id);

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
