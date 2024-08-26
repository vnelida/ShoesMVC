using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shoes.Datos.Repositories
{
	public class SizeRepository : ISizeRepository
	{
		private readonly ShoesDbContext context;
        public SizeRepository(ShoesDbContext _context)
        {
			context = _context;	
        }

        public void Agregar(Size size)
		{
			context.Sizes.Add(size);
		}

		public void Borrar(Size size)
		{
			context.Sizes.Remove(size);
		}

		public void Editar(Size size)
		{
			context.Sizes.Update(size);
		}

		public bool EstaRelacionado(Size size)
		{
			return context.ShoeSizes.Any(s=>s.SizeId== size.SizeId);
		}

		public bool Existe(Size size)
		{
			if (size.SizeId == 0)
			{
				return context.Sizes.Any(s => s.SizeNumber == size.SizeNumber);
			}
			return context.Sizes.Any(s => s.SizeNumber == size.SizeNumber &&
				s.SizeId != size.SizeId);
		}

		public int GetCantidad()
		{
			return context.Sizes.Count();
		}

		public List<Size> GetLista()
		{
			return context.Sizes.AsNoTracking().ToList();
		}

		public List<Size> GetListaPaginada(int page, int pageSize)
		{
			
			return context.Sizes.Select(s => new Size
								{
									SizeId = s.SizeId,
									SizeNumber = s.SizeNumber,
								}).OrderBy(s => s.SizeNumber).
								Skip((page-1) * pageSize).Take(pageSize).ToList();

		}

		public List<Shoe> GetShoe(Size? sizeEnDB)
		{
			if (sizeEnDB != null)
			{
				var shoes = context.Shoes
			.Where(s => s.ShoesSizes.Any(ss => ss.SizeId == sizeEnDB.SizeId))
			.Include(s => s.Brand)
			.Include(s => s.ColorN)
			.Include(s => s.Sport)
			.Include(s => s.Genre)
			.ToList();

				return shoes;
			}
			return null;
		}

		public Size? GetSizePorId(int id)
		{
			return context.Sizes.SingleOrDefault(s => s.SizeId == id);
		}

		public Size? GetSizePorIDxShoe(int id, bool incluyeShoe = false)
		{
			var query = context.Sizes;
			if (incluyeShoe)
			{
				return query.Include(s => s.ShoesSizes)
					.ThenInclude(ss=>ss.ShoeN).FirstOrDefault(s => s.SizeId == id);
			}
			return query.FirstOrDefault(s => s.SizeId == id);
		}

		public Size GetSizetPorNombre(string sizeN)
		{
			decimal _sizeNumber = decimal.Parse(sizeN);

			var resultado= context.Sizes
				.FirstOrDefault(s => s.SizeNumber == _sizeNumber);

			return resultado;

		}
	}
}
