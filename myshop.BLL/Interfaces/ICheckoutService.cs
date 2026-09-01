
using myshop.BLL.DTOs.Checkout;

namespace myshop.BLL.Interfaces
{
    public interface ICheckoutService
    {
        Task<int> CreateOrderAsync(CheckoutDto dto, string userId);
    }
}
