using System.ComponentModel.DataAnnotations;

namespace MarketLink.Admin.Models.Enums;

public enum MapProvider
{
    OpenStreetMap = 0,
    [Display(Name = "Google Maps")] GoogleMaps = 1
}

public enum AnnouncementAudience
{
    [Display(Name = "All users")] AllUsers = 0,
    Farmers = 1,
    Customers = 2
}

public enum ReportType
{
    [Display(Name = "Orders summary")] OrdersSummary = 0,
    [Display(Name = "Revenue by market")] RevenueByMarket = 1,
    [Display(Name = "Most active farmers")] MostActiveFarmers = 2,
    [Display(Name = "Full platform report")] FullPlatform = 3
}
