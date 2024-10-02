using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IShoesRepository:IGenericRepository<Shoe>
	{
		void Update(Shoe shoe);
		bool Exist(Shoe shoe);
		bool IsRelated(int id);

	}
}
