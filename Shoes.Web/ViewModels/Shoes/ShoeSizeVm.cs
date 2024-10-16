using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shoes.Web.ViewModels.Shoes
{
    public class ShoeSizeVm
    {
        public int ShoeSizeId { get; set; }
        public int ShoeId { get; set; }
        public int SizeId { get; set; } 
        public decimal SizeNumber { get; set; }
        public int QuantityInStock { get; set; }
        public List<int> SizeIds { get; set; } = new List<int>();
        public List<SelectListItem> Sizes { get; set; } = null!;
    }
}
