using FCG.Users.Domain.Entities;
using FCG.Users.Application.Interface.Repository.Base;
using FCG.Users.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FCG.Users.Infrastructure.Repository.Base
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id) =>
            _dbSet.Find(id);

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task<T?> GetByOrDefaultIdAsync(int id) =>
            await _dbSet.FirstOrDefaultAsync(a => a.Id == id);

        public bool GetByIdExists(int id) =>
            _dbSet.Any(a => a.Id == id);

        public async Task<bool> GetByIdExistsAsync(int id) =>
            await _dbSet.AnyAsync(a => a.Id == id);

        public async Task<bool> ExistsByAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).AnyAsync();

        public T Add(T entidade)
        {
            entidade.SetDateCreation(DateTime.Now, DateTime.Now);
            _dbSet.Add(entidade);
            _context.SaveChanges();
            return entidade;
        }

        public async Task<T> AddAsync(T entidade)
        {
            entidade.SetDateCreation(DateTime.Now, DateTime.Now);
            await _dbSet.AddAsync(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public void Delete(int id)
        {
            _dbSet.Remove(GetById(id));
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var entidade = await GetByIdAsync(id);
            _dbSet.Remove(entidade);
            await _context.SaveChangesAsync();
        }

        public void Update(T entidade)
        {
            entidade.SetDateUpdate(DateTime.Now);
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entidade)
        {
            entidade.SetDateUpdate(DateTime.Now);
            _context.Entry(entidade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> All => _context.Set<T>();
    }
}
