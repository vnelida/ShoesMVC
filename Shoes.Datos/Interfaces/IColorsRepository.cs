using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System.Linq.Expressions;

namespace Shoes.Datos.Interfaces
{
	public interface IColorsRepository:IGenericRepository<Color>
	{
		void Editar(Color color);
		bool Existe(Color color);
		bool EstaRelacionado(int id);
	}
}
