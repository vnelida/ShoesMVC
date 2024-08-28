using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entidades.ViewModels.Brand
{
	public class BrandEditVm
	{
        public int BrandId { get; set; }
        [Required(ErrorMessage ="{0} is required")]
        [StringLength(50, ErrorMessage ="{0} must be between {2} and {1} characters", MinimumLength =3)]
        [DisplayName("Name")]
        public string? BrandName { get; set; }
    }
}
