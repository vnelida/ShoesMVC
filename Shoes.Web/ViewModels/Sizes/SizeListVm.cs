using Shoes.Entidades;
using System.ComponentModel;

namespace Shoes.Web.ViewModels.Sizes
{
	public class SizeListVm
	{
		public int SizeId { get; set; }
		[DisplayName("Size")]
		public decimal SizeNumber { get; set; }
	}
}
