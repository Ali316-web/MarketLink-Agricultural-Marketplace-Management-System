using System.ComponentModel.DataAnnotations;

namespace MarketLink.Admin.Models.Entities;

public class Category
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    [Display(Name = "Category name")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
