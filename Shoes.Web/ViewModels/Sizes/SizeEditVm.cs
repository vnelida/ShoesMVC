using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes.Web.ViewModels.Sizes
{
	public class SizeEditVm
	{
		public int SizeId { get; set; }
		[Required(ErrorMessage = "Size is required")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a size")]
		[DisplayName("Size")]
		public decimal SizeNumber { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> ShoesSizes { get; set; } = null!;
	}
}
