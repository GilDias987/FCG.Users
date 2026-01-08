using FCG.Users.Application.Interface.Repository.Base;
using FCG.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.Interface.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> VerifyUserEmailAsync(string email);
        Task<User?> UserEmailAsync(string email);
        Task<User?> GetUserAsync(int id);
        Task<bool> GetByEmailExistsAsync(int usuarioId, string email);
        Task<List<User>> GetAllUsers();
    }
}
