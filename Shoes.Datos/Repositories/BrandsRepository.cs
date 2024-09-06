using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class BrandsRepository : GenericRepository<Brand>, IBrandsRepository
	{
		private readonly ShoesDbContext _db;
		public BrandsRepository(ShoesDbContext db):base(db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public void Editar(Brand brand)
		{
			_db.Brands.Update(brand);
		}

		public bool EstaRelacionado(int id)
		{
			return _db.Shoes.Any(s => s.BrandId == id);
		}

		public bool Existe(Brand brand)
		{
			if (brand.BrandId == 0)
			{
				return _db.Brands.Any(b => b.BrandName == brand.BrandName);
			}
			return _db.Brands.Any(b => b.BrandName == brand.BrandName &&
				b.BrandId != brand.BrandId);
		}

	}
}
