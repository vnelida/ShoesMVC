using Shoes.Entidades;
using System.ComponentModel;

namespace Shoes.Web.ViewModels.Shoes
{
	public class ShoeListVm
	{
		public int ShoeId { get; set; }
		[DisplayName("Model Name")]
		public string Model { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public string ColorN { get; set; } = null!;
		public string Brand { get; set; } = null!;
		public string Sport { get; set; } = null!;
		public string Genre { get; set; } = null!;
	}
}
