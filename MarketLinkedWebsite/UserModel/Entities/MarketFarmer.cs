using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class MarketFarmer
    {
        public int MarketId { get; set; }

        [ForeignKey(nameof(MarketId))]
        public virtual Market Market { get; set; } = null!;

        public int FarmerProfileId { get; set; }

        [ForeignKey(nameof(FarmerProfileId))]
        public virtual FarmerProfile FarmerProfile { get; set; } = null!;

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
        public string StallNumber { get; set; } = string.Empty;
    }
}