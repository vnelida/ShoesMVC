using Shoes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Datos.Interfaces
{
	public interface ISizeRepository
	{
		void Agregar(Size size);
		void Borrar(Size size);
		void Editar(Size size);
		bool EstaRelacionado(Size size);
		bool Existe(Size size);
		int GetCantidad();
		List<Size> GetLista();
		List<Size> GetListaPaginada(int page, int pageSize);
		List<Shoe> GetShoe(Size? sizeEnDB);
		Size? GetSizePorId(int id);
		Size? GetSizePorIDxShoe(int id, bool incluyeShoe=false);
		Size GetSizetPorNombre(string sizeN);
	}
}
