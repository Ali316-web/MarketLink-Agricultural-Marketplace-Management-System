
using MarketLink.Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketLink.Admin.Models.Entities;

public class Announcement
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(2000)]
    [DataType(DataType.MultilineText)]
    public string Body { get; set; } = string.Empty;

    public AnnouncementAudience Audience { get; set; } = AnnouncementAudience.AllUsers;

    [DataType(DataType.DateTime)]
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
}
