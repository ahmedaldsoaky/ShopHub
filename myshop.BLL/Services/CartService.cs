using Microsoft.AspNetCore.Http;
using myshop.BLL.DTOs.Cart;
using myshop.BLL.Interfaces;
using myshop.Common.Extensions;

namespace myshop.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartKey = "Cart";

        public CartService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        private ISession Session =>
            _httpContextAccessor.HttpContext!.Session;

        public List<CartItemDto> GetCart()
        {
            return Session.GetObject<List<CartItemDto>>(CartKey)
               ?? new List<CartItemDto>();
        }

        public void AddItem(CartItemDto item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existing is not null)
                existing.Quantity++;
            else
                cart.Add(item);
            Session.SetObject(CartKey, cart);
        }

        public void RemoveItem(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item is not null)
                cart.Remove(item);
            Session.SetObject(CartKey, cart);
        }

        public void ClearCart()
        {
            Session.Remove(CartKey);
        }

        public void IncreaseQuantity(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item is not null)
                item.Quantity++;
            Session.SetObject(CartKey, cart);
        }

        public void DecreaseQuantity(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item is not null)
                item.Quantity--;
            Session.SetObject(CartKey, cart);
        }

        public decimal GetOrderTotal()
        {
            var cart = GetCart();
            decimal total = cart.Sum(c => c.Total);
            return total;
        }

        public int GetTotalItems()
        {
            return GetCart().Count;
        }
    }
}
