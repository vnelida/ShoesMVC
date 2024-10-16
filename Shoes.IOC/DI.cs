using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Datos.Repositories;
using Shoes.Servicios.Interface;
using Shoes.Servicios.Servicios;

namespace Shoes.IOC
{
    public static class DI
    {
        public static void ConfigurarServicio(IServiceCollection servicios, IConfiguration configuration)
        {

            servicios.AddScoped<IColorsService, ColorsService>();
            servicios.AddScoped<IGenresService, GenresService>();
            servicios.AddScoped<ISportsService, SportsService>();
            servicios.AddScoped<IBrandsService, BrandsService>();
            servicios.AddScoped<IShoesService, ShoesService>();
			servicios.AddScoped<ISizeService, SizeService>();
            servicios.AddScoped<IShoeSizeService, ShoeSizeService>();

            servicios.AddScoped<IShoeSizesRepository, ShoeSizesRepository>();
			servicios.AddScoped<ISizeRepository, SizeRepository>();
			servicios.AddScoped<IShoesRepository, ShoesRepository>();
            servicios.AddScoped<IColorsRepository, ColorsRepository>();
            servicios.AddScoped<IGenresRepository, GenresRepository>();
            servicios.AddScoped<ISportsRepository, SportsRepository>();
            servicios.AddScoped<IBrandsRepository, BrandsRepository>();

            servicios.AddScoped<IUnitOfWork, UnitOfWork>();
            servicios.AddDbContext<ShoesDbContext>(optiones =>
            {
                optiones.UseSqlServer(configuration.GetConnectionString("MyConn") );
            });

        }
    }
}
