using Shoes.Web.ViewModels.Shoes;
using System.ComponentModel;
using X.PagedList;

namespace Shoes.Web.ViewModels.Genres
{
	public class GenreDetailsVm
	{
		public int GenreId { get; set; }
		[DisplayName("Genre")]
		public string?  GenreName { get; set; }
        [DisplayName("Shoes Qty.")]
        public int ShoesQuantity { get; set; }
		public List<ShoeListVm>? Shoes { get; set; }
	}
}
