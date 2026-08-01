using myshop.BLL.DTOs.Cart;

namespace myshop.Web.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemDto> Items { get; set; } = [];

        public decimal OrderTotal {  get; set; }
    }
}
