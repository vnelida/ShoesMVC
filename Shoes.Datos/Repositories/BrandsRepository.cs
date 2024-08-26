using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class BrandsRepository : IBrandsRepository
	{
		private readonly ShoesDbContext context;
		public BrandsRepository(ShoesDbContext _context)
		{
			context = _context;
		}
		public void Borrar(Brand brand)
		{
			context.Brands.Remove(brand);
		}

		public void Editar(Brand brand)
		{
			context.Brands.Update(brand);
		}

		public bool EstaRelacionado(Brand brand)
		{
			return context.Shoes.Any(s => s.BrandId == brand.BrandId);
		}

		public bool Existe(Brand brand)
		{
			if (brand.BrandId == 0)
			{
				return context.Brands.Any(b => b.BrandName == brand.BrandName);
			}
			return context.Brands.Any(b => b.BrandName == brand.BrandName &&
				b.BrandId != brand.BrandId);
		}

		public Brand? GetBrandPorId(int idEditar)
		{
			return context.Brands.SingleOrDefault(c => c.BrandId == idEditar);
		}

		public Brand? GetBrandPorNombre(string brandN)
		{
			return context.Brands
				.FirstOrDefault(b => b.BrandName == brandN);
		}

		public int GetCantidad()
		{
			return context.Brands.Count();
		}

		public List<Brand> GetLista()
		{
			return context.Brands.AsNoTracking().ToList();
		}

		public List<Brand> GetListaOrdenada(Orden orden)
		{
			IQueryable<Brand> query = context.Brands
										.Select(b => new Brand
										{
											BrandId = b.BrandId,
											BrandName = b.BrandName,
										});
			switch (orden)
			{
				case Orden.AZ:
					return query.OrderBy(b => b.BrandName).ToList();
				default:
					return query.OrderByDescending(b => b.BrandName).ToList();


			}
		}

		public List<Brand> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			IQueryable<Brand> query = context.Brands
										.Select(b => new Brand
										{
											BrandId = b.BrandId,
											BrandName = b.BrandName,
										});
			switch (orden)
			{
				case Orden.AZ:
					query = query.OrderBy(b => b.BrandName);
					break;
				default:
					query = query.OrderByDescending(b => b.BrandName);
					break;
			}
			return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
		}

		public List<Shoe>? GetShoe(Brand brand)
		{
			if (brand != null)
			{
				context.Entry(brand)
					.Collection(b => b.Shoes)
					.Query()
					.Include(s => s.Brand)
					.Include(s => s.ColorN)
					.Include(s => s.Sport)
					.Include(s => s.Genre)
					.Load();
				var shoes=brand.Shoes.ToList();
				return shoes;
			} return null;
		}

		public void Guardar(Brand brand)
		{
			context.Brands.Add(brand);
		}


	}
}
