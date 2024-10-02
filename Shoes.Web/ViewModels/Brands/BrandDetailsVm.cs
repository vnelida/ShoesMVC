using Shoes.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;

namespace Shoes.Web.ViewModels.Brands
{
    public class BrandDetailsVm
    {
        public int BrandId { get; set; }
        [DisplayName("Brand")]
        public string? BrandName { get; set; }
        [DisplayName("Shoes Quantity")]
        public int ShoesQuantity { get; set; }
        public List<ShoeListVm>? Shoes { get; set; }
    }
}
