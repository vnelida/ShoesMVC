using Shoes.Entidades;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IBrandsRepository
	{
		void Borrar(Brand brand);
		void Editar(Brand brand);
		bool EstaRelacionado(Brand brand);
		bool Existe(Brand brand);
		Brand? GetBrandPorId(int idEditar);
		Brand GetBrandPorNombre(string brandN);
		int GetCantidad();
		List<Brand> GetLista();
		List<Brand> GetListaOrdenada(Orden orden);
		List<Brand> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ);
		List<Shoe>? GetShoe(Brand brand);
		void Guardar(Brand brand);
	}
}
