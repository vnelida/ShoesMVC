using Microsoft.EntityFrameworkCore;
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
        public static IServiceProvider ConfigurarServicio()
        {
            var servicio = new ServiceCollection();

            servicio.AddScoped<IColorsService, ColorsService>();
            servicio.AddScoped<IGenresService, GenresService>();
            servicio.AddScoped<ISportsService, SportsService>();
            servicio.AddScoped<IBrandsService, BrandsService>();
            servicio.AddScoped<IShoesService, ShoesService>();
			servicio.AddScoped<ISizeService, SizeService>();

			servicio.AddScoped<ISizeRepository, SizeRepository>();
			servicio.AddScoped<IShoesRepository, ShoesRepository>();
            servicio.AddScoped<IColorsRepository, ColorsRepository>();
            servicio.AddScoped<IGenresRepository, GenresRepository>();
            servicio.AddScoped<ISportsRepository, SportsRepository>();
            servicio.AddScoped<IBrandsRepository, BrandsRepository>();

            servicio.AddScoped<IUnitOfWork, UnitOfWork>();
            servicio.AddDbContext<ShoesDbContext>(optiones =>
            {
                optiones.UseSqlServer(@"Data Source=.; 
                        Initial Catalog=ShoesTP1.2024; 
                        Trusted_Connection=true; 
                        TrustServerCertificate=true;");
            });

            return servicio.BuildServiceProvider();
        }
    }
}
