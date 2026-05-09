using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Queries.GetAllUserGroup
{

    public class GetAllUserGroupQueryHandler : IRequestHandler<GetAllUserGroupQuery, List<UserGroupDto>>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ICacheService _cache;

        private const string CacheKey = "user-groups:all";

        public GetAllUserGroupQueryHandler(IUserGroupRepository userGroupRepository, ICacheService cache)
        {
            _userGroupRepository = userGroupRepository;
            _cache = cache;
        }

        public async Task<List<UserGroupDto>> Handle(GetAllUserGroupQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cache.GetAsync<List<UserGroupDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var lstUserGroup = _userGroupRepository.All
                .Select(x => new UserGroupDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            if (!lstUserGroup.Any())
                throw new ArgumentException("Nenhum registro encontrado.");

            // 💾 3. salva no cache
            await _cache.SetAsync(CacheKey, lstUserGroup, TimeSpan.FromMinutes(10));

            return lstUserGroup;
        }

    }
}
