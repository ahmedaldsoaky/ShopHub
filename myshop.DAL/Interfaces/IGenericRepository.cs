using System.Linq.Expressions;

namespace myshop.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Expression<Func<T, object>>[] includes = null,
        bool isTracking = true);
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> CheckIfEntityExistsAsync(Expression<Func<T, bool>> expr);
    }
}
