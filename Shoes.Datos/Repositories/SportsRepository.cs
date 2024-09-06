using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Repositories
{
	public class SportsRepository : GenericRepository<Sport>, ISportsRepository
	{
		private readonly ShoesDbContext _db;
		public SportsRepository(ShoesDbContext db) : base(db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}
		public bool Exist(Sport sport)
		{
			if (sport.SportId == 0)
			{
				return _db!.Sports.Any(c => c.SportName == sport.SportName);
			}
			return _db!.Sports.Any(t => t.SportName == sport.SportName && t.SportId != sport.SportId);
		}

		public bool IsRelated(int id)
		{
			return _db!.Shoes.Any(s => s.SportId == id);
		}

		public void Update(Sport sport)
		{
			_db!.Sports.Update(sport);
		}
	}
}
