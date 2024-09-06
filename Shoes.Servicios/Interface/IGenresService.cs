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
    public interface IGenresService
    {
		IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>>? filter = null, Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null, string? propertiesNames = null);
		Genre? Get(Expression<Func<Genre, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
		bool Exist(Genre genre);
		bool IsRelated(int id);
		void Save(Genre genre);
		void Delete(Genre genre);
	}
}
