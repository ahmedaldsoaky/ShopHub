
namespace myshop.BLL.DTOs.Checkout
{
    public class CheckoutDto
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
    }
}
