using System.ComponentModel;

namespace Shoes.Web.ViewModels.Shoes
{
    public class ShoeHomeIndexVm
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal CashPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string ColorN { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Sport { get; set; } = null!;
        public string Genre { get; set; } = null!;
    }
}
