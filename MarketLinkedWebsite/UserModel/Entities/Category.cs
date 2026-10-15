using MarketLinkWebsite.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmersMarket.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Relationship
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}