using System.ComponentModel;

namespace Shoes.Web.ViewModels.Colors
{
	public class ColorListVm
	{
		public int ColorId { get; set; }
		[DisplayName("Color")]
		public string ColorName { get; set; } = null!;
	}
}
