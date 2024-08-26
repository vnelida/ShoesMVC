using Shoes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
	public interface ISizeService
	{
		List<Size> GetLista();
		void Guardar(Size size);
		void Borrar(Size size);
		bool Existe(Size size);
		Size? GetSizePorId(int id);
		Size? GetSizePorIDxShoe(int id, bool incluyeShoe=false);
		bool EstaRelacionado(Size size);
		Size GetSizePorNombre(string sizeN);
		int GetCantidad();
		List<Size> GetListaPaginada(int page, int pageSize);
		List<Shoe> GetShoes(Size? sizeEnDB);
	}
}
