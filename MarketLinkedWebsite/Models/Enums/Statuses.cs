using System.ComponentModel.DataAnnotations;

namespace MarketLink.Admin.Models.Enums;

public enum FarmerStatus
{
    Pending = 0,
    Approved = 1,
    Suspended = 2
}

public enum CustomerStatus
{
    Active = 0,
    Inactive = 1
}

public enum OrderStatus
{
    Placed = 0,
    Accepted = 1,
    [Display(Name = "Ready for pickup")] ReadyForPickup = 2,
    Completed = 3,
    Cancelled = 4
}
