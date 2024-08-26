using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;

namespace Shoes.Servicios.Interface
{
	public interface IShoesService
	{
		int GetCantidad(Func<Shoe, bool>? filtro = null);
		void Guardar(Shoe shoe, List<Size>? sizes=null);
		void GuardarConTalle(Shoe shoe, Size size);
		void Borrar(Shoe shoe);
		List<Shoe> GetLista();
		IEnumerable<object> GetListaAnonima();
		bool Existe(Shoe shoe);
		List<ShoeListDto> GetListaDto();
		Shoe? GetShoePorId(int shoeId);
		List<ShoeListDto> GetListaPaginadaOrdenadaFiltrada(int page, int pageSize, Orden? orden = null, Brand? brandFiltro = null, Color? colorFiltro = null, Genre? genreFiltro = null, Sport? sportFiltro = null);
		List<Size>? GetSizePorShoes(int shoeId);

		void EditarSs(int shoeSizeId, int stock);
		List<SizeDetailDto>? GetSizeDetalle(int shoeId);


		bool ExisteRelacion(Shoe shoe, Size size);
		void AsignarSizeAShoe(Shoe nuevoShoe, Size nuevoSize);
		List<ShoeListDto> ShoesSinTalle();
		void Editar(Shoe shoe, int? sizeId = null);
		IEnumerable<IGrouping<int, Shoe>> GetShoesXGenre();
		IEnumerable<IGrouping<int, Shoe>> GetShoesXBrands();
		void AgregarStock(int shoeId, int sizeId, int unidades);
		List<ShoeSize> GetListaShoesSizes(int shoeId);
		ShoeSize GetShoeSize(int shoeId, int sizeId);
	}
}
