using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Shoes.Web.ViewModels.Shoes
{
	public class ShoeFilterVm
	{
        public IPagedList<ShoeListVm>? Shoes { get; set; }
		public List<SelectListItem>? Brands { get; set; }
	}
}
