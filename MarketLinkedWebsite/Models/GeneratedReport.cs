
using MarketLink.Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketLink.Admin.Models.Entities;

/// <summary>A record of a report an admin generated.</summary>
public class GeneratedReport
{
    public int Id { get; set; }

    public ReportType Type { get; set; }

    [Required]
    public string GeneratedById { get; set; } = string.Empty;
    public ApplicationUser? GeneratedBy { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
