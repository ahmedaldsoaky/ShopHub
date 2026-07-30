using Microsoft.EntityFrameworkCore;
using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using System.Linq.Expressions;

namespace myshop.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // CancellationToken used to cancel the operation if needed,
        // but it's not used in this implementation.

        private readonly DbSet<T> table;
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        
        public async Task<IReadOnlyList<TResult>> GetProjectedAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            int pageNumber = 1,
            int pageSize = 10,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = table;

            if (filter is not null)
                query = query
                    .Where(filter);

            if (orderBy is not null)
                query = orderBy(query);

            return await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToListAsync();
        }
        
        public async Task<TResult?> GetProjectedFirstOrDefaultAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? condition = null)
        {
            IQueryable<T> query = table;
            if (condition is not null)
                query = query.Where(condition);
            return await query
                .AsNoTracking()
                .Select(selector)
                .FirstOrDefaultAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expr)
            => await table.AnyAsync(expr);

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = table;
            if (filter is not null)
                query = query
                        .Where(filter);
            return await query.CountAsync();
        }


        public async Task AddAsync(T entity)
            => await table.AddAsync(entity);
        public void Update(T entity)
            => table.Update(entity);
        public void Delete(T entity)
            => table.Remove(entity);

    }
}
