using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;

namespace SmartOnApp.WebAPI.RepositoryLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<T> GetAsync(Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        void UpdateAsync(T entity);
        Task DeleteAsync(int id);
        void DeleteRangeAsync(IEnumerable<T> entities);   
    }
}
