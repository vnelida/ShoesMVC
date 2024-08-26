using Shoes.Entidades;
using Shoes.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Interface
{
    public interface IBrandsService
    {
        List<Brand> GetLista();
        void Guardar(Brand brand);
        void Borrar(Brand brand);
        bool Existe(Brand brand);
        Brand? GetBrandPorId(int idEditar);
        bool EstaRelacionado(Brand brand);
        Brand GetBrandPorNombre(string brandN);
		int GetCantidad();
        List <Shoe>? GetShoes(Brand brand);
		List<Brand> GetListaOrdenada(Orden orden);
		List<Brand> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ);

	}
}
