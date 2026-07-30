using System.Linq.Expressions;

namespace myshop.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);

        Task<IReadOnlyList<TResult>> GetProjectedAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            int pageNumber = 1,
            int pageSize = 10,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<TResult?> GetProjectedFirstOrDefaultAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? filter = null);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expr);

        Task<int> CountAsync(
            Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
