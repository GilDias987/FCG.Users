using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Queries.GetAllUserGroup
{
    public class GetAllUserGroupQueryHandler : IRequestHandler<GetAllUserGroupQuery, List<UserGroupDto>>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "user-groups:all";

        public GetAllUserGroupQueryHandler(IUserGroupRepository userGroupRepository, ICacheService cacheService)
        {
            _userGroupRepository = userGroupRepository;
            _cacheService = cacheService;
        }

        public async Task<List<UserGroupDto>> Handle(GetAllUserGroupQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<List<UserGroupDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var lstUserGroup = _userGroupRepository.All
                .Select(x => new UserGroupDto { Id = x.Id, Name = x.Name }).ToList();

            if (!lstUserGroup.Any())
                throw new ArgumentException("Nenhum registro encontrado.");

            await _cacheService.SetAsync(CacheKey, lstUserGroup, TimeSpan.FromMinutes(10));

            return lstUserGroup;
        }

    }
}
