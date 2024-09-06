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
    public interface ISportsService
    {
		IEnumerable<Sport> GetAll(Expression<Func<Sport, bool>>? filter = null, Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null, string? propertiesNames = null);
		Sport? Get(Expression<Func<Sport, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
		bool Exist(Sport sport);
		bool IsRelated(int id);
		void Save(Sport sport);
		void Delete(Sport sport);
	}
}
