using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes.Web.ViewModels.Genres
{
	public class GenreEditVm
	{
		public int GenreId { get; set; }
		[Required(ErrorMessage = "{0} is required")]
		[StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 3)]
		[DisplayName("Genre")]
		public string? GenreName { get; set; }
	}
}
