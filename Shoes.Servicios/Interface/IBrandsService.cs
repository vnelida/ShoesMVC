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
    public interface IBrandsService
    {
		IEnumerable<Brand> GetAll(Expression<Func<Brand, bool>>? filter = null, Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null, string? propertiesNames = null);
		Brand? Get(Expression<Func<Brand, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
		bool Existe(Brand brand);
		bool EstaRelacionado(int id);
		void Save(Brand brand);
		void Delete(Brand brand);

	}
}
