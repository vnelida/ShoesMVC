using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class ColorsRepository : IColorsRepository
	{
		private readonly ShoesDbContext _context;
		public ColorsRepository(ShoesDbContext dbContext)
		{
			_context = dbContext;
		}
		public void Agregar(Color color)
		{
			_context.Colors.Add(color);
		}

		public void Borrar(Color color)
		{
			_context.Colors.Remove(color);
		}

		public void Editar(Color color)
		{
			_context.Colors.Update(color);
		}

		public bool EstaRelacionado(Color color)
		{
			return _context.Shoes.Any(s => s.ColorId == color.ColorId);
		}

		public bool Existe(Color color)
		{
			
			if (color.ColorId == 0)
			{
				return _context.Colors.Any(c => c.ColorName == color.ColorName);
			}
			return _context.Colors.Any(t => t.ColorName == color.ColorName && t.ColorId != color.ColorId);
		}

		public Color? GetColorPorId(int idEditar)
		{
			return _context.Colors.SingleOrDefault(c => c.ColorId == idEditar);
		}

		public Color? GetColorPorNombre(string color)
		{
			return _context.Colors
				.FirstOrDefault(c => c.ColorName == color);
		}

		public List<Color> GetLista()
		{
			return _context.Colors
				.OrderBy(c => c.ColorId)
				.ToList();
		}


		public int GetCantidad()
		{
			return _context.Colors.Count();
		}

		public List<Color> GetListaOrdenada(Orden orden)
		{
			IQueryable<Color> query = _context.Colors
										.Select(c => new Color
										{
											ColorId = c.ColorId,
											ColorName = c.ColorName,
										});
			switch (orden)
			{
				case Orden.AZ:
					return query.OrderBy(c => c.ColorName).ToList();
				default:
					return query.OrderByDescending(c => c.ColorName).ToList();


			}
		}

		public List<Color> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			IQueryable<Color> query = _context.Colors
										.Select(c => new Color
										{
											ColorId = c.ColorId,
											ColorName = c.ColorName,
										});
			switch (orden)
			{
				case Orden.AZ:
					query = query.OrderBy(c => c.ColorName);
					break;
				default:
					query = query.OrderByDescending(c => c.ColorName);
					break;
			}
			return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
		}

		public List<Shoe>? GetShoe(Color? colorEnDB)
		{
			if (colorEnDB != null)
			{
				_context.Entry(colorEnDB)
					.Collection(b => b.Shoes)
					.Query()
					.Include(s => s.Brand)
					.Include(s => s.ColorN)
					.Include(s => s.Sport)
					.Include(s => s.Genre)
					.Load();
				var shoes = colorEnDB.Shoes.ToList();
				return shoes;
			}
			return null;
		}
	}
}
