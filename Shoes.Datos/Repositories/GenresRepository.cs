using Shoes.Datos.Interfaces;
using Shoes.Entidades;

namespace Shoes.Datos.Repositories
{
	public class GenresRepository : GenericRepository<Genre>, IGenresRepository
	{
		private readonly ShoesDbContext? _db;
		public GenresRepository(ShoesDbContext db) : base(db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public bool Exist(Genre genre)
		{
			if (genre.GenreId == 0)
			{
				return _db!.Genres.Any(c => c.GenreName == genre.GenreName);
			}
			return _db!.Genres.Any(t => t.GenreName == genre.GenreName && t.GenreId != genre.GenreId);
		}

		public bool IsRelated(int id)
		{
			return _db!.Shoes.Any(s => s.GenreId == id);
		}

		public void Update(Genre genre)
		{
			_db!.Genres.Update(genre);
		}
	}
}
