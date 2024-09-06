using Shoes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
	public interface ISizeService
	{
		IEnumerable<Size> GetAll(Expression<Func<Size, bool>>? filter = null, Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null, string? propertiesNames = null);
		Size? Get(Expression<Func<Size, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
		bool Exist(Size size);
		bool IsRelated(int id);
		void Save(Size size);
		void Delete(Size size);
	}
}
