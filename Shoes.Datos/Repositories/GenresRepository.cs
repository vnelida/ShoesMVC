using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class GenresRepository : IGenresRepository
	{
		private readonly ShoesDbContext context;
		public GenresRepository(ShoesDbContext _context)
		{
			context = _context;
		}
		public void Borrar(Genre genre)
		{
			context.Genres.Remove(genre);
		}

		public void Editar(Genre genre)
		{
			context.Genres.Update(genre);
		}

		public bool EstaRelacionado(Genre genre)
		{
			return context.Shoes.Any(s => s.GenreId == genre.GenreId);
		}

		public bool Existe(Genre genre)
		{
			if (genre.GenreId == 0)
			{
				return context.Genres.Any(g => g.GnereName == genre.GnereName);
			}
			return context.Genres.Any(b => b.GnereName == genre.GnereName &&
				b.GenreId != genre.GenreId);
		}

		public Genre? GetGenrePorId(int idEditar)
		{
			return context.Genres.SingleOrDefault(g => g.GenreId == idEditar);
		}

		public Genre? GetGenrePorNombre(string genreN)
		{
			return context.Genres
			   .FirstOrDefault(g => g.GnereName == genreN);
		}

		public List<Genre> GetLista()
		{
			return context.Genres.AsNoTracking().ToList();
		}

		public void Guardar(Genre genre)
		{
			context.Genres.Add(genre);
		}

		public int GetCantidad()
		{
			return context.Genres.Count();
		}


		public List<Genre> GetListaOrdenada(Orden orden)
		{
			IQueryable<Genre> query = context.Genres
										.Select(g => new Genre
										{
											GenreId = g.GenreId,
											GnereName = g.GnereName,
										});
			switch (orden)
			{
				case Orden.AZ:
					return query.OrderBy(g => g.GnereName).ToList();
				default:
					return query.OrderByDescending(g => g.GnereName).ToList();


			}
		}

		public List<Genre> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			IQueryable<Genre> query = context.Genres
										.Select(g => new Genre
										{
											GenreId = g.GenreId,
											GnereName = g.GnereName,
										});
			switch (orden)
			{
				case Orden.AZ:
					query = query.OrderBy(g => g.GnereName);
					break;
				default:
					query = query.OrderByDescending(g => g.GnereName);
					break;
			}
			return query.Skip((page-1) * pageSize).Take(pageSize).ToList();
		}

		public List<Shoe>? GetShoe(Genre genre)
		{
			if (genre != null)
			{
				context.Entry(genre)
					.Collection(b => b.Shoes)
					.Query()
					.Include(s => s.Brand)
					.Include(s => s.ColorN)
					.Include(s => s.Sport)
					.Include(s => s.Genre)
					.Load();
				var shoes = genre.Shoes.ToList();
				return shoes;
			}
			return null;
		}
	}
}
