using AutoMapper;
using Shoes.Entidades;
using Shoes.Web.ViewModels.Color;

namespace Shoes.Web.Mappings
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
            LoadColorsMapping();
        }

		private void LoadColorsMapping()
		{
			CreateMap<Color, ColorEditVm>().ReverseMap();
		}
	}
}
