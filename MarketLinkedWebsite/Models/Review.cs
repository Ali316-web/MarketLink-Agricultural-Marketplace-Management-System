using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketLink.Admin.Models.Entities;

/// <summary>A customer's review of a farmer's stall.</summary>
public class Review
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required, StringLength(1000)]
    public string Comment { get; set; } = string.Empty;

    [StringLength(250)]
    [Display(Name = "Flag reason")]
    public string? FlagReason { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped] public bool IsFlagged => !string.IsNullOrWhiteSpace(FlagReason);
}
