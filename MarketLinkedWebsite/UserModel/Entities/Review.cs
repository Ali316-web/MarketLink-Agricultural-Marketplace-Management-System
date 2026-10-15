using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        public int? FarmerProfileId { get; set; }

        [ForeignKey(nameof(FarmerProfileId))]
        public virtual FarmerProfile? FarmerProfile { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}