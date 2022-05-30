using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly SmartOnDbContext _db;

        public Repository(SmartOnDbContext db) => _db = db;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _db.Set<T>().AsNoTracking().ToListAsync();
            return entities;
        }//GetallAsync

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);

            // Or
            // var entity = await db.Set<T>().FindAsync(id);
            // return entity;
        }//GetByIdAsync

        public async Task<bool> CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            int numRowsEffected = await _db.SaveChangesAsync();
            return numRowsEffected > 0;
        }//CreateAsync

        public async Task<bool> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            int numRowsEffected = await _db.SaveChangesAsync();
            return numRowsEffected > 0;
        }//UpdateAsync

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            _db.Set<T>().Remove(entityToDelete);
            int numRowEffected = await _db.SaveChangesAsync();
            return numRowEffected > 0;
        }//DeleteAsync
    }
}
