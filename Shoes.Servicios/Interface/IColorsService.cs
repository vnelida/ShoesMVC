using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
    public interface IColorsService
    {
		IEnumerable<Color> GetAll(Expression<Func<Color, bool>>? filter = null, Func<IQueryable<Color>, IOrderedQueryable<Color>>? orderBy = null, string? propertiesNames = null);
		Color? Get(Expression<Func<Color, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
		bool Existe(Color color);
		bool EstaRelacionado(int id);
		void Save(Color color);
		void Delete(Color color);



	}
}
