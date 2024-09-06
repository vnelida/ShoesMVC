using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using System.Drawing;

namespace Shoes.Datos.Repositories
{
	public class ShoesRepository : GenericRepository<Shoe>, IShoesRepository
	{
		private readonly ShoesDbContext _context;
		public ShoesRepository(ShoesDbContext context) : base(context)
		{
			_context = context;
		}

		public bool Exist(Shoe shoe)
		{
			if (shoe.ShoeId == 0)
			{
				return _context.Shoes.Any(
					p => p.Model == shoe.Model &&
					p.Description == shoe.Description &&
					p.ColorId == shoe.ColorId &&
					p.GenreId == shoe.GenreId &&
					p.SportId == shoe.SportId &&
					p.BrandId == shoe.BrandId);

			}
			return _context.Shoes.Any(
				p => p.Model == shoe.Model &&
				p.Description == shoe.Description &&
				p.SportId == shoe.SportId &&
				p.ColorId == shoe.ColorId &&
				p.SportId == shoe.SportId &&
				p.GenreId == shoe.GenreId &&
				p.ShoeId != shoe.ShoeId);
		}

		public bool IsRelated(int id)
		{
            var existeRelacion = _context.Shoes.Any(s => s.ShoeId == id && s.ShoesSizes.Any());

            return existeRelacion;
        }

        public void Update(Shoe shoe)
		{
			_context.Shoes.Update(shoe);
		}

	}
}
