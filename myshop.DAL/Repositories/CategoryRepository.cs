using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;

namespace myshop.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
