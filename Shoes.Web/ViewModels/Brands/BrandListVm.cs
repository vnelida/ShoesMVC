using System.ComponentModel;

namespace Shoes.Web.ViewModels.Brands
{
	public class BrandListVm
	{
		public int BrandId { get; set; }
		[DisplayName("Brand")]
		public string BrandName { get; set; } = null!;
	}
}
