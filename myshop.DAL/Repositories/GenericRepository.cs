using Microsoft.EntityFrameworkCore;
using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using System.Linq.Expressions;

namespace myshop.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DbSet<T> table;
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await table.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Expression<Func<T, object>>[] includes = null,
        bool isTracking = true)
        {
            IQueryable<T> query = table;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
        
            if (orderBy != null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
            => await table.FindAsync(id);

        public async Task AddAsync(T entity)
            => await table.AddAsync(entity);
        public void Update(T entity)
            => table.Update(entity);
        public void Delete(T entity)
            => table.Remove(entity);

        public async Task<bool> CheckIfEntityExistsAsync(Expression<Func<T, bool>> expr)
            => await table.AnyAsync(expr);
    }
}
