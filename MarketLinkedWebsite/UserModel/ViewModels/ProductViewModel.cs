using System.ComponentModel.DataAnnotations;
using LocalFarmersMarket.Enums;
using Microsoft.AspNetCore.Http;

namespace LocalFarmersMarket.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required, Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Required]
        public UnitType Unit { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, Range(0, 10000)]
        public int QuantityAvailable { get; set; }

        public bool IsOrganic { get; set; }
        public bool IsAvailable { get; set; } = true;

        public string? ExistingImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}