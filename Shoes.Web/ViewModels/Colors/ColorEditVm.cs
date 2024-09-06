using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes.Web.ViewModels.Color
{
	public class ColorEditVm
	{
		public int ColorId { get; set; }
		[Required(ErrorMessage = "{0} is required")]
		[StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 3)]
		[DisplayName("Color Name")]
		public string? ColorName { get; set; }
	}
}
