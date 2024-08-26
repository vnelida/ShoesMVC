using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface ISportsRepository
	{
		void Borrar(Sport sport);
		void Editar(Sport sport);
		bool EstaRelacionado(Sport sport);
		bool Existe(Sport sport);
		int GetCantidad();
		List<Sport> GetLista();
		List<Sport> GetListaOrdenada(Orden orden);
		List<Sport> GetListaPaginada(int page, int pageSize, Orden? orden);
		List<Shoe>? GetShoe(Sport sport);
		Sport? GetSportPorId(int idEditar);
		Sport? GetSportPorNombre(string sportEdit);
		void Guardar(Sport sport);
	}
}
