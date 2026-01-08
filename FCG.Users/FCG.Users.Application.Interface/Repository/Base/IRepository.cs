using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FCG.Users.Domain.Entities;

namespace FCG.Users.Application.Interface.Repository.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task<T?> GetByOrDefaultIdAsync(int id);
        bool GetByIdExists(int id);
        Task<bool> GetByIdExistsAsync(int id);
        Task<bool> ExistsByAsync(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entidade);
        void Delete(int id);
        Task DeleteAsync(int id);
        IQueryable<T> All { get; }
    }
}
