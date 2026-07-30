using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace myshop.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<Product?> GetByIdWithCategoryAsync(int id)
        //{
        //    return await _context.Products
        //    .Select(p => new Product)
        //    .FirstOrDefaultAsync(p => p.Id == id);
        //}
    }
}
