using myshop.BLL.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface ICartService
    {
        List<CartItemDto> GetCart();

        void AddItem(CartItemDto item);

        void RemoveItem(int productId);

        void IncreaseQuantity(int productId);

        void DecreaseQuantity(int productId);

        void ClearCart();

        decimal GetOrderTotal();
        int GetTotalItems();
    }
}
