using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cache;

        private const string CacheKey = "users:all";

        public GetAllUserQueryHandler(IUserRepository userRepository, ICacheService cache)
        {
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cache.GetAsync<List<UserDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var lstUsers = (await _userRepository.GetAllUsers())
                .Select(s => new UserDto{ Id = s.Id, Name = s.Name, Email = s.Email, UserGroupId = s.UserGroupId, Group = s.UserGroup.Name }).ToList();

            if (!lstUsers.Any())
                throw new ArgumentException("Nenhum registro encontrado.");

            await _cache.SetAsync(CacheKey, lstUsers, TimeSpan.FromMinutes(10));

            return lstUsers;
        }
    }
}
