using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IGenresRepository:IGenericRepository<Genre>
	{
		void Update(Genre genre);
		bool Exist(Genre genre);
		bool IsRelated(int id);
	}
}
