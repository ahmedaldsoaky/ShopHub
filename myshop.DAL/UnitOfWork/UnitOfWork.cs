using myshop.DAL.Context;
using myshop.DAL.Interfaces;

namespace myshop.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IOrderRepository Orders { get; }
        
        public UnitOfWork(ApplicationDbContext context, IProductRepository Products, ICategoryRepository Categories, IOrderRepository Orders)
        {
            this.context = context;
            this.Products = Products;
            this.Categories = Categories;
            this.Orders = Orders;
        }

        public async Task<int> SaveAsync()
            => await context.SaveChangesAsync();

        public void Dispose()
            => context.Dispose();
    }
}
