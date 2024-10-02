using Shoes.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;

namespace Shoes.Web.ViewModels.Colors
{
	public class ColorDetailsVm
	{
		public int ColorId { get; set; }
		[DisplayName("Genre")]
		public string? ColorName { get; set; }
        [DisplayName("Shoes Quantity")]
        public int ShoesQuantity { get; set; }
		public List<ShoeListVm>? Shoes { get; set; }
	}
}
