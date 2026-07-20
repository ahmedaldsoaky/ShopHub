using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using myshop.DAL.Repositories;

namespace myshop.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        
        public UnitOfWork(ApplicationDbContext context, IProductRepository Products, ICategoryRepository Categories)
        {
            this.context = context;
            this.Products = Products;
            this.Categories = Categories;
        }

        public async Task<int> SaveAsync()
            => await context.SaveChangesAsync();

        public void Dispose()
            => context.Dispose();
    }
}
