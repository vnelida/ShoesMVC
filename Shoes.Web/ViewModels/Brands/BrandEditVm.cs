using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes.Web.ViewModels.Brands
{
	public class BrandEditVm
	{
		public int BrandId { get; set; }
		[Required(ErrorMessage = "{0} is required")]
		[StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 3)]
		[DisplayName("Name")]
		public string? BrandName { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Remove Image")]
        public bool RemoveImage { get; set; }
        public bool Suspended { get; set; }
    }
}
