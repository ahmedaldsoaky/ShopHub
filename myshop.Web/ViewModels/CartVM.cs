using myshop.BLL.DTOs.Cart;

namespace myshop.Web.ViewModels
{
    public class CartVM
    {
        public List<CartItemDto> Items { get; set; } = [];
        public decimal OrderTotal {  get; set; }
        public int TotalItems { get; set; }
    }
}
