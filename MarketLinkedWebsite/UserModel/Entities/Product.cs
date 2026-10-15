using LocalFarmersMarket.Enums;
using MarketLinkWebsite.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public UnitType Unit { get; set; } = UnitType.Kg;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsOrganic { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public int FarmerProfileId { get; set; }

        [ForeignKey(nameof(FarmerProfileId))]
        public virtual FarmerProfile FarmerProfile { get; set; } = null!;

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        // Relationships
        public virtual Inventory? Inventory { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<FavoriteProduct> FavoritedByUsers { get; set; } = new List<FavoriteProduct>();
    }
}