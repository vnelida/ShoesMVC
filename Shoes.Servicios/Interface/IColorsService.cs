using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
    public interface IColorsService
    {
        List<Color> GetLista();
        void Guardar(Color color);
        void Borrar(Color color);
        bool Existe(Color color);
        Color? GetColorPorId(int idEditar);
        Color? GetColorPorNombre(string color);
        bool EstaRelacionado(Color color);
		int GetCantidad();
		List<Color> GetListaOrdenada(Orden orden);
		List<Color> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ);
		List<Shoe>? GetShoes(Color? colorEnDB);
	}
}
