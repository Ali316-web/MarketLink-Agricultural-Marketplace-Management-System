using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        [Required]
        public int QuantityAvailable { get; set; }

        public int ReorderThreshold { get; set; } = 5;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}