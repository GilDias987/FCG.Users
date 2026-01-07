using FCG.Users.Application.Interface.Repository;
using FCG.Users.Domain.Entities;
using FCG.Users.Infrastructure.Context;
using FCG.Users.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Infrastructure.Repository
{
    public class UserGroupRepository : EFRepository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsNameAsync(string nome)
        {
            return await _dbSet.AnyAsync(a => a.Name.Trim().ToLower() == nome.Trim().ToLower());
        }

        public async Task<bool> VerifyExistsGroupAsync(string nomeGrupo)
        {
            var group = await _dbSet.FirstOrDefaultAsync(g => g.Name.ToLower() == nomeGrupo.ToLower());
            return group != null ? true : false;
        }

        public async Task<IList<UserGroup>> ListUserGroupAsync()
        {
            return await _dbSet.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
