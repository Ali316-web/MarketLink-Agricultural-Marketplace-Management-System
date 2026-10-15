using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketLink.Admin.Models.Enums;

namespace MarketLink.Admin.Models.Entities;

public class Farmer
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    [Display(Name = "Stall name")]
    public string StallName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    [Display(Name = "Contact person")]
    public string ContactName { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string? Phone { get; set; }

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = string.Empty;

    /// <summary>Null means the farmer is not assigned to a market yet ("Unassigned").</summary>
    [Display(Name = "Market")]
    public int? MarketId { get; set; }
    public Market? Market { get; set; }

    public FarmerStatus Status { get; set; } = FarmerStatus.Pending;

    [DataType(DataType.Date)]
    [Display(Name = "Joined")]
    public DateOnly JoinedOn { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    // Calculated from Orders - Include(f => f.Orders) before reading these.
    [NotMapped] public int TotalOrders => Orders.Count;

    [NotMapped, DataType(DataType.Currency)]
    public decimal TotalRevenue => Orders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.Total);
}
