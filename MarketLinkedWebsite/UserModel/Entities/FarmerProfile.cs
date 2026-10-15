using LocalFarmersMarket.Enums;
using MarketLinkWebsite.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class FarmerProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required, MaxLength(150)]
        public string FarmName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public FarmerStatus Status { get; set; } = FarmerStatus.PendingApproval;

        public double Rating { get; set; } = 0.0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public virtual ICollection<MarketFarmer> MarketFarmers { get; set; } = new List<MarketFarmer>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<FavoriteFarmer> FavoritedByUsers { get; set; } = new List<FavoriteFarmer>();
    }
}