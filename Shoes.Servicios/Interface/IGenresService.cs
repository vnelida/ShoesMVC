using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
    public interface IGenresService
    {
        List<Genre> GetLista();
        void Guardar(Genre genre);
        void Borrar(Genre genre);
        bool Existe(Genre genre);
        Genre? GetGenrePorId(int idEditar);
        bool EstaRelacionado(Genre genre);
        Genre GetGenrePorNombre(string genreN);
		int GetCantidad();
		List<Shoe>? GetShoes(Genre genre);
		List<Genre> GetListaOrdenada(Orden orden);
		List<Genre> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ);
	}
}
