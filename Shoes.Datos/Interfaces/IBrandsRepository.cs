using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IBrandsRepository : IGenericRepository<Brand>
	{
		void Editar(Brand brand);
		bool EstaRelacionado(int id);
		bool Existe(Brand brand);
	}
}
