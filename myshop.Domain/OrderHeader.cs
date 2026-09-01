
namespace myshop.Entities.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public required string ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }//

        public decimal TotalPrice { get; set; }

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }

        public string? TrakcingNumber { get; set; }
        public string? Carrier { get;set; }

        public DateTime? PaymentDate { get; set; }

        //Stripe Properties

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        //User Data
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? PhoneNumber { get; set; }

        // Navigation property for OrderDetails
        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();

    }
}
