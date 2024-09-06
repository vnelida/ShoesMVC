using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
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


		public string ColorN { get; set; }
		public string Brand { get; set; }
		public string Sport { get; set; }
		public string Genre { get; set; }
    }
}
