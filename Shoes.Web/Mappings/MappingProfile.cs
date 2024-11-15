﻿using AutoMapper;
using Shoes.Entidades;
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
            CreateMap<Brand, BrandDetailsVm>();
        }

        private void LoadColorsMapping()
        {
            CreateMap<Color, ColorEditVm>().ReverseMap();
            CreateMap<Color, ColorListVm>();
            CreateMap<Color, ColorDetailsVm>();
        }
        private void LoadGenresMapping()
        {
            CreateMap<Genre, GenreEditVm>().ReverseMap();
            CreateMap<Genre, GenreListVm>();
            CreateMap<Genre, GenreDetailsVm>();
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
               opt => opt.MapFrom(src => src.Genre.GenreName));
            CreateMap<Shoe, ShoeEditVm>().ReverseMap();
            CreateMap<Shoe, ShoeHomeIndexVm>().
                ForMember(dest => dest.ColorN, opt => opt.MapFrom(c => c.ColorN.ColorName))
               .ForMember(dest => dest.Brand, opt => opt.MapFrom(b => b.Brand.BrandName))
               .ForMember(dest => dest.Sport, opt => opt.MapFrom(t => t.Sport.SportName))
               .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(p => p.Price))
               .ForMember(dest => dest.CashPrice, opt => opt.MapFrom(p => p.Price * 0.9m));
            CreateMap<Shoe, ShoeHomeDetailsVm>().
                ForMember(dest => dest.ColorN, opt => opt.MapFrom(c => c.ColorN.ColorName))
               .ForMember(dest => dest.Brand, opt => opt.MapFrom(b => b.Brand.BrandName))
               .ForMember(dest => dest.Sport, opt => opt.MapFrom(t => t.Sport.SportName))
               .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(p => p.Price))
               .ForMember(dest => dest.CashPrice, opt => opt.MapFrom(p => p.Price * 0.9m))
               .ForMember(dest => dest.ShoeSize, opt => opt.MapFrom(src => src.ShoesSizes.ToList()));

            CreateMap<ShoeSize, ShoeSizeVm>()
               .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))  
            .ForMember(dest => dest.SizeNumber, opt => opt.MapFrom(src => src.SizeN.SizeNumber))
            .ForMember(dest => dest.SizeIds, opt => opt.MapFrom(src => src.SizeId)); 


        }
    }
}
