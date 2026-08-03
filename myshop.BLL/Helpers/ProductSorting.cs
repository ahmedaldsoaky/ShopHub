using myshop.Entities.Models;
namespace myshop.BLL.Helpers
{
    internal class ProductSorting
    {
        public static Func<IQueryable<Product>, IOrderedQueryable<Product>>? 
            GetSorting(string? sortColumn, string? sortDirection)
        {
            return (sortColumn?.ToLower(), sortDirection?.ToLower()) switch
            {
                ("name", "desc") => q => q.OrderByDescending(p => p.Name),
                ("name", _) => q => q.OrderBy(p => p.Name),

                ("categoryname", "desc") => q => q.OrderByDescending(p => p.Category!.Name),
                ("categoryname", _) => q => q.OrderBy(p => p.Category!.Name),

                ("price", "desc") => q => q.OrderByDescending(p => p.Price),
                ("price", _) => q => q.OrderBy(p => p.Price),

                ("description", "desc") => q => q.OrderByDescending(p => p.Description),
                ("description", _) => q => q.OrderBy(p => p.Description),

                _ => q => q.OrderBy(p => p.Id),
            };
        }
    }
}
