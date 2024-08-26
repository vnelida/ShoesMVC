using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
    public interface ISportsService
    {
        List<Sport> GetLista();
        void Guardar(Sport sport);
        void Borrar(Sport sport);
        bool Existe(Sport sport);
        Sport? GetSportPorId(int idEditar);
        bool EstaRelacionado(Sport sport);
        Sport? GetSportPorNombre(string sportEdit);
		int GetCantidad();
		List<Shoe>? GetShoes(Sport sport);
		List<Sport> GetListaOrdenada(Orden orden);
		List<Sport> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ);
	}
}
