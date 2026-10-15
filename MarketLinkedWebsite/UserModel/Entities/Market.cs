using MarketLinkWebsite.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmersMarket.Entities
{
    public class Market
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string OperatingHours { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Many-to-Many Relationship with FarmerProfile via MarketFarmer
        public virtual ICollection<MarketFarmer> MarketFarmers { get; set; } = new List<MarketFarmer>();
    }
}