using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;

namespace Shoes.Datos.Interfaces
{
	public interface IShoesRepository
	{
		void Agregar(Shoe shoe);
		void AgregarSizeAShoe(Shoe shoe, List<Size> sizes);
		void AgregarShoeSize(ShoeSize nuevaRelacion);
		void Borrar(Shoe shoe);
		void Editar(Shoe shoe);
		void Editar(Shoe shoe, int? sizeId = null);
		bool Existe(Shoe shoe);
		bool ExisteRelacion(Shoe shoe, Size size);
		int GetCantidad(Func<Shoe, bool>? filtro);
		List<Shoe> GetLista();
		List<ShoeListDto> GetListaDto();
		List<ShoeListDto> GetListaPaginadaOrdenadaFiltrada(int page, int pageSize, Orden? orden, Brand? brandFiltro, Color? colorFiltro, Genre? genreFiltro, Sport? sportFiltro);
		Shoe? GetShoePorId(int shoeId);
		List<ShoeListDto> GetShoesSinTallle();
		List<Size>? GetSizePorShoes(int shoeId);
		void EliminarRelacion(Shoe shoe);
		IEnumerable<IGrouping<int, Shoe>> GetShoesXGenre();
		IEnumerable<IGrouping<int, Shoe>> GetShoesXMarca();
		List<SizeDetailDto>? GetSizeDetail(int shoeId);
		void AgregarStock(int shoeId, int sizeId, int unidades);
		List<ShoeSize> GetListaShoesSizes(int shoeId);
		ShoeSize GetShoeSize(int shoeId, int sizeId);
		void EditarSs(int shoeSizeId, int stock);
	}
}
