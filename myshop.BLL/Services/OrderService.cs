using myshop.BLL.DTOs.Order;
using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;

namespace myshop.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<OrderReadDto>> GetUserOrdersAsync(string userId)
        {
            return await _unitOfWork.Orders.GetProjectedListAsync(
                order => new OrderReadDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    OrderStatus = order.OrderStatus,
                    PaymentStatus = order.PaymentStatus
                },
                order => order.ApplicationUserId == userId,
                order => order.OrderByDescending(x => x.OrderDate));
        }
    }
}
