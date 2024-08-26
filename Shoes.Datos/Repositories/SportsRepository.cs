using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class SportsRepository : ISportsRepository
	{
		private readonly ShoesDbContext context;
		public SportsRepository(ShoesDbContext _context)
		{
			context = _context;
		}
		public void Borrar(Sport sport)
		{
			context.Sports.Remove(sport);
		}

		public void Editar(Sport sport)
		{
			context.Sports.Update(sport);
		}

		public bool EstaRelacionado(Sport sport)
		{
			return context.Shoes.Any(s => s.SportId == sport.SportiD);
		}

		public bool Existe(Sport sport)
		{
			if (sport.SportiD == 0)
			{
				return context.Sports.Any(s => s.SportName == sport.SportName);
			}
			return context.Sports.Any(s => s.SportName == sport.SportName &&
				s.SportiD != sport.SportiD);
		}

		public List<Sport> GetLista()
		{
			return context.Sports.AsNoTracking().ToList();
		}

		public Sport? GetSportPorId(int idEditar)
		{
			return context.Sports.SingleOrDefault(s => s.SportiD == idEditar);
		}

		public Sport? GetSportPorNombre(string sportN)
		{
			return context.Sports
				.FirstOrDefault(s => s.SportName == sportN);
		}

		public void Guardar(Sport sport)
		{
			context.Sports.Add(sport);
		}

		public int GetCantidad()
		{
			return context.Sports.Count();
		}


		public List<Sport> GetListaOrdenada(Orden orden)
		{
			IQueryable<Sport> query = context.Sports
										.Select(s => new Sport
										{
											SportiD = s.SportiD,
											SportName = s.SportName,
										});
			switch (orden)
			{
				case Orden.AZ:
					return query.OrderBy(s => s.SportName).ToList();
				default:
					return query.OrderByDescending(s => s.SportName).ToList();


			}
		}

		public List<Sport> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			IQueryable<Sport> query = context.Sports
										.Select(s => new Sport
										{
											SportiD = s.SportiD,
											SportName = s.SportName,
										});
			switch (orden)
			{
				case Orden.AZ:
					query = query.OrderBy(s => s.SportName);
					break;
				default:
					query = query.OrderByDescending(s => s.SportName);
					break;
			}
			return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
		}

		public List<Shoe>? GetShoe(Sport sport)
		{
			if (sport != null)
			{
				context.Entry(sport)
					.Collection(b => b.Shoes)
					.Query()
					.Include(s => s.Brand)
					.Include(s => s.ColorN)
					.Include(s => s.Sport)
					.Include(s => s.Genre)
					.Load();
				var shoes = sport.Shoes.ToList();
				return shoes;
			}
			return null;
		}
	}
}
