using MarketLink.Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MarketLink.Admin.Models.Entities;

public class Order
{
    public int Id { get; set; }

    /// <summary>Public order reference such as "ML-1042" (unique).</summary>
    [Required, StringLength(20)]
    [Display(Name = "Order")]
    public string Number { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }

    [Range(typeof(decimal), "0", "1000000")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Placed;

    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
}
