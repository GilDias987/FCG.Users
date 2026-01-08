using FCG.Users.Application.Interface.Repository;
using FCG.Users.Infrastructure.Context;
using FCG.Users.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using FCG.Users.Domain.Entities;
namespace FCG.Users.Infrastructure.Repository
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> VerifyUserEmailAsync(string email)
        {
            var usuario = await _dbSet.FirstOrDefaultAsync(g => g.Email.ToLower() == email.ToLower());
            return usuario != null ? true : false;
        }

        public async Task<bool> GetByEmailExistsAsync(int usuarioId, string email)
        {
            return await _dbSet
                .Include(i => i.UserGroup)
               .AnyAsync(a => a.Id != usuarioId && a.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> UserEmailAsync(string email)
        {
            return await _dbSet.Include(x => x.UserGroup).FirstOrDefaultAsync(g => g.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _dbSet.Include(x => x.UserGroup).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbSet.Include(x => x.UserGroup).ToListAsync();
        }
    }
}
