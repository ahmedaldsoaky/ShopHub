using myshop.BLL.DTOs.Checkout;
using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;

namespace myshop.BLL.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public CheckoutService(
            IUnitOfWork unitOfWork,
            ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }
        public async Task<int> CreateOrderAsync(CheckoutDto dto, string userId)
        {
            var cart = _cartService.GetCart();

            if (!cart.Any())
                throw new InvalidOperationException("Cannot create an order with an empty cart.");
            
            var order = new OrderHeader
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.Sum(item => item.Price * item.Quantity),
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                PhoneNumber = dto.PhoneNumber,
                OrderStatus = "Pending",
                PaymentStatus = "Pending"
            };

            foreach (var item in cart)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Count = item.Quantity
                });
            }

            await _unitOfWork.Orders.AddAsync(order);
            
            await _unitOfWork.SaveAsync();

            _cartService.ClearCart();

            return order.Id;
        }
    }
}
