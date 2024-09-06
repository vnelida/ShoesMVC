using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;
using System.Linq.Expressions;

namespace Shoes.Servicios.Interface
{
	public interface IShoesService
	{
		IEnumerable<Shoe> GetAll(Expression<Func<Shoe, bool>>? filter = null, Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null, string? propertiesNames = null);
		Shoe? Get(Expression<Func<Shoe, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
		bool Exist(Shoe shoe);
		bool IsRelated(int id);
		void Save(Shoe shoe);
		void Delete(Shoe shoe);
	}
}
