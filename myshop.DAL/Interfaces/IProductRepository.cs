using myshop.Entities.Models;

namespace myshop.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //Task<Product?> GetByIdWithCategoryAsync(int id);
    }
}
