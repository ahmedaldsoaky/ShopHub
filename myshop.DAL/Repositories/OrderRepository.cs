using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;

namespace myshop.DAL.Repositories
{
    internal class OrderRepository : GenericRepository<OrderHeader>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
