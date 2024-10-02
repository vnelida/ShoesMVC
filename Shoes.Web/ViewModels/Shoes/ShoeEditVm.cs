using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shoes.Web.ViewModels.Shoes
{
	public class ShoeEditVm
	{
		public int ShoeId { get; set; }
		[Required(ErrorMessage = "{0} is required")]
		[StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
		[DisplayName("Model Name")]

		public string Model { get; set; } = null!;
		[Required(ErrorMessage = "{0} is required")]
		[StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
		[DisplayName("Description")]

		public string Description { get; set; } = null!;


		public string? ImageUrl {  get; set; }
        public IFormFile? ImageFile { get; set; }
        [Display(Name="Remove Image")]
        public bool RemoveImage { get; set; }
        public bool Suspended { get; set; }




        [Required(ErrorMessage = "{0} is required")]
		[DisplayName("Price")]

		public decimal Price { get; set; }
		[Required(ErrorMessage = "Sport is required")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a sport")]
		[DisplayName("Sport")]
		public int SportId { get; set; }
		[Required(ErrorMessage = "Genre is required")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a genre")]
		[DisplayName("Genre")]
		public int GenreId { get; set; }
		[Required(ErrorMessage = "Brand is required")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a brand")]
		[DisplayName("Brand")]

		public int BrandId { get; set; }
		[Required(ErrorMessage = "Color is required")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a color")]
		[DisplayName("Color")]
		public int ColorId { get; set; }
		[ValidateNever]
		public List<SelectListItem> Colors { get; set; } = null!;
		[ValidateNever]
		public List<SelectListItem> Brands { get; set; } = null!;
		[ValidateNever]
		public List<SelectListItem> Genres { get; set; } = null!;
		[ValidateNever]
		public List<SelectListItem> Sports { get; set; } = null!;

	}
}
