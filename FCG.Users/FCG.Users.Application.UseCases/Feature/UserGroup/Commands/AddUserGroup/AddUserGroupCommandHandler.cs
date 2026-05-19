using FCG.Users.Application.Dto.UserGroup;
using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.Interface.Service;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.UserGroup.Commands.AddUserGroup
{
    public class AddUserGroupCommandHandler : IRequestHandler<AddUserGroupCommand, UserGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "user-groups:all";

        public AddUserGroupCommandHandler(IUserGroupRepository userGroupRepository, ICacheService cacheService)
        {
            _userGroupRepository = userGroupRepository;
            _cacheService = cacheService;
        }

        public async Task<UserGroupDto> Handle(AddUserGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objUserGroup = await _userGroupRepository.AddAsync(new Domain.Entities.UserGroup(request.Name));

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return new UserGroupDto() 
                { 
                    Id   = objUserGroup.Id, 
                    Name = objUserGroup.Name 
                };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o Grupo de usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
