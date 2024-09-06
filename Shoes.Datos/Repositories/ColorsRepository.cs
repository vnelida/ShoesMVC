using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class ColorsRepository : GenericRepository<Color>, IColorsRepository
	{
		private readonly ShoesDbContext _db;
		public ColorsRepository(ShoesDbContext db) : base(db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}
		
		public void Editar(Color color)
		{
			_db.Colors.Update(color);
		}

		public bool EstaRelacionado(int id)
		{
			return _db.Shoes.Any(s => s.ColorId == id);
		}


		public bool Existe(Color color)
		{
			
			if (color.ColorId == 0)
			{
				return _db.Colors.Any(c => c.ColorName == color.ColorName);
			}
			return _db.Colors.Any(t => t.ColorName == color.ColorName && t.ColorId != color.ColorId);
		}


	}
}
