using FCG.Users.Application.Interface.Repository.Base;
using FCG.Users.Domain.Entities;

namespace FCG.Users.Application.Interface.Repository
{

    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        Task<bool> ExistsNameAsync(string nome);
        public Task<bool> VerifyExistsGroupAsync(string nomeGrupo);
        public Task<IList<UserGroup>> ListUserGroupAsync();
    }
}
