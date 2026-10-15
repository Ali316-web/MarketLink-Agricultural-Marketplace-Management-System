using LocalFarmersMarket.Enums;
using MarketLinkWebsite.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string OrderNumber { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public string ShippingAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "COD";

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Relationships
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}