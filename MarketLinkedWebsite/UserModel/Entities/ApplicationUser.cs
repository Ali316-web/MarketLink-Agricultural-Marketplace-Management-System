using MarketLinkWebsite.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace LocalFarmersMarket.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

       
        public virtual FarmerProfile? FarmerProfile { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<FavoriteFarmer> FavoriteFarmers { get; set; } = new List<FavoriteFarmer>();
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}