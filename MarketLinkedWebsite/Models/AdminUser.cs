using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MarketLink.Admin.Models.Entities;

/// <summary>Back-office account (sign in / register). Email, password hash, etc. come from IdentityUser.</summary>
public class ApplicationUser : IdentityUser
{
    [Required, StringLength(100)]
    [Display(Name = "Full name")]
    public string FullName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<GeneratedReport> GeneratedReports { get; set; } = new List<GeneratedReport>();
}
