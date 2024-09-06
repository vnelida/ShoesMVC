using System.ComponentModel;

namespace Shoes.Web.ViewModels.Genres
{
	public class GenreListVm
	{
		public int GenreId { get; set; }
		[DisplayName("Genre")]
		public string GenreName { get; set; } = null!;
	}
}
