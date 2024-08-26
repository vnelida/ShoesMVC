using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IColorsRepository
	{
		void Agregar(Color color);
		void Borrar(Color color);
		void Editar(Color color);
		bool Existe(Color color);
		Color? GetColorPorId(int idEditar);
		Color? GetColorPorNombre(string color);
		List<Color> GetLista();
		bool EstaRelacionado(Color color);
		List<Color> GetListaPaginada(int page, int pageSize, Orden? orden);
		List<Color> GetListaOrdenada(Orden orden);
		int GetCantidad();
		List<Shoe>? GetShoe(Color? colorEnDB);
	}
}
