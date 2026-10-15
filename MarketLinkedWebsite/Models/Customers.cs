
using MarketLink.Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketLink.Admin.Models.Entities;

public class Customer
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    public CustomerStatus Status { get; set; } = CustomerStatus.Active;

    [DataType(DataType.Date)]
    [Display(Name = "Joined")]
    public DateOnly JoinedOn { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    [NotMapped] public int TotalOrders => Orders.Count;
}
