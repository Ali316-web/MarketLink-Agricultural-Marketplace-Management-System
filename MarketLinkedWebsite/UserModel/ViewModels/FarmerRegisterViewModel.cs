using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LocalFarmersMarket.ViewModels
{
    public class FarmerRegisterViewModel
    {
        // User Details
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // Farm Details
        [Required, MaxLength(150)]
        public string FarmName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        public IFormFile? ProfilePicture { get; set; }
    }
}