using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketLink.Admin.Models.Entities;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Range(typeof(decimal), "0.01", "100000")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    /// <summary>Selling unit, e.g. "lb", "dozen", "jar".</summary>
    [Required, StringLength(30)]
    public string Unit { get; set; } = string.Empty;

    /// <summary>Set by a customer report or an admin; shown in content moderation. Null/empty = not flagged.</summary>
    [StringLength(250)]
    [Display(Name = "Flag reason")]
    public string? FlagReason { get; set; }

    [NotMapped] public bool IsFlagged => !string.IsNullOrWhiteSpace(FlagReason);
}
