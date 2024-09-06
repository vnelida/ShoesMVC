using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Size = Shoes.Entidades.Size;

namespace Shoes.Datos.Repositories
{
	public class SizeRepository : GenericRepository<Size>, ISizeRepository
	{
		private readonly ShoesDbContext _db;
		public SizeRepository(ShoesDbContext db) : base(db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}
		        
		public bool Exist(Size size)
		{
			if (size.SizeId == 0)
			{
				return _db.Sizes.Any(s => s.SizeNumber == size.SizeNumber);
			}
			return _db.Sizes.Any(s => s.SizeNumber == size.SizeNumber &&
				s.SizeId != size.SizeId);
		}
		public bool IsRelated(int id)
		{
			return _db.ShoeSizes.Any(s => s.SizeId == id);
		}

		public void Update(Size size)
		{
			_db.Sizes.Update(size);
		}
	}
}
