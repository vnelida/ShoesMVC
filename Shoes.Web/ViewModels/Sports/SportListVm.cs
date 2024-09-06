using System.ComponentModel;

namespace Shoes.Web.ViewModels.Sports
{
	public class SportListVm
	{
		public int SportId { get; set; }
		[DisplayName("Sport")]
		public string SportName { get; set; } = null!;

	}
}
