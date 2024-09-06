using AutoMapper;
using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Web.ViewModels.Brands;
using Shoes.Web.ViewModels.Color;
using Shoes.Web.ViewModels.Colors;
using Shoes.Web.ViewModels.Genres;
using Shoes.Web.ViewModels.Shoes;
using Shoes.Web.ViewModels.Sizes;
using Shoes.Web.ViewModels.Sports;

namespace Shoes.Web.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			LoadColorsMapping();
			LoadBrandsMapping();
			LoadGenresMapping();
			LoadSportsMapping();
			LoadShoeMapping();
			LoadSizeMapping();
		}

		private void LoadSizeMapping()
		{
			CreateMap<Size, SizeEditVm>().ReverseMap();
			CreateMap<Size, SizeListVm>();
		}

		private void LoadBrandsMapping()
		{
			CreateMap<Brand, BrandEditVm>().ReverseMap();
			CreateMap<Brand, BrandListVm>();
		}

		private void LoadColorsMapping()
		{
			CreateMap<Color, ColorEditVm>().ReverseMap();
			CreateMap<Color, ColorListVm>();
		}
		private void LoadGenresMapping()
		{
			CreateMap<Genre, GenreEditVm>().ReverseMap();
			CreateMap<Genre, GenreListVm>();
		}
		private void LoadSportsMapping()
		{
			CreateMap<Sport, SportsEditVm>().ReverseMap();
			CreateMap<Sport, SportListVm>();
		}
		private void LoadShoeMapping()
		{
			CreateMap<Shoe, ShoeListVm>().
			   ForMember(dest => dest.ColorN,
			   opt => opt.MapFrom(c => c.ColorN.ColorName))
			   .ForMember(dest => dest.Brand,
			   opt => opt.MapFrom(b => b.Brand.BrandName))
			   .ForMember(dest => dest.Sport,
			   opt => opt.MapFrom(t => t.Sport.SportName))
			   .ForMember(dest => dest.Genre,
			   opt => opt.MapFrom(g => g.Genre.GenreName));
			CreateMap<Shoe, ShoeEditVm>().ReverseMap();
		}
	}
}
