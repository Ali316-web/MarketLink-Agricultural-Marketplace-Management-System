using System.ComponentModel.DataAnnotations;

namespace LocalFarmersMarket.ViewModels
{
    public class OrderCheckoutViewModel
    {
        public CartViewModel Cart { get; set; } = new CartViewModel();

        [Required]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = "Cash on Delivery";
        public string OrderNotes { get; set; } = string.Empty;
    }
} 