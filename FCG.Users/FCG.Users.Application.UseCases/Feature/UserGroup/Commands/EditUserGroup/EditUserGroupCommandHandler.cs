using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.EditUserGroup
{
    public class EditUserGroupCommandHandler : IRequestHandler<EditUserGroupCommand, UserGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "user-groups:all";

        public EditUserGroupCommandHandler(IUserGroupRepository userGroupRepository, ICacheService cacheService)
        {
            _userGroupRepository = userGroupRepository;
            _cacheService = cacheService;
        }

        public async Task<UserGroupDto> Handle(EditUserGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _userGroupRepository.GetByIdAsync(request.Id);
                group.Initialize(request.Name);

            await _userGroupRepository.UpdateAsync(group);

            // Remover cache Redis.
            await _cacheService.RemoveAsync(CacheKey);

            return new UserGroupDto() 
            { 
                Id   = group.Id, 
                Name = group.Name 
            };
        }
    }
}
