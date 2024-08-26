using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IGenresRepository
	{
		void Borrar(Genre genre);
		void Editar(Genre genre);
		bool EstaRelacionado(Genre genre);
		bool Existe(Genre genre);
		int GetCantidad();
		Genre? GetGenrePorId(int idEditar);
		Genre GetGenrePorNombre(string genreN);
		List<Genre> GetLista();
		List<Genre> GetListaOrdenada(Orden orden);
		List<Genre> GetListaPaginada(int page, int pageSize, Orden? orden);
		List<Shoe>? GetShoe(Genre genre);
		void Guardar(Genre genre);
	}
}
