using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface ISportsRepository:IGenericRepository<Sport>
	{
		void Update(Sport sport);
		bool Exist(Sport sport);
		bool IsRelated(int id);
	}
}
