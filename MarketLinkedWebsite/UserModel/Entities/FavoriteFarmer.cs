using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class FavoriteFarmer
    {
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public int FarmerProfileId { get; set; }

        [ForeignKey(nameof(FarmerProfileId))]
        public virtual FarmerProfile FarmerProfile { get; set; } = null!;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}