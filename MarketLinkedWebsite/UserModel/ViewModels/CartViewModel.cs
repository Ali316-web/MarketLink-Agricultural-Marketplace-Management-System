namespace LocalFarmersMarket.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int FarmerId { get; set; }
        public string FarmName { get; set; } = string.Empty;

        public decimal SubTotal => Price * Quantity;
    }

    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal GrandTotal => Items.Sum(x => x.SubTotal);
    }
}