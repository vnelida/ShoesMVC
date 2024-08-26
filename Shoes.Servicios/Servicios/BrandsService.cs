using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class BrandsService : IBrandsService
    {
        private readonly IBrandsRepository repository;
        private readonly IUnitOfWork _unitOfWork;
        public BrandsService(IBrandsRepository _repository,  IUnitOfWork unitOfWork)
        {
                repository = _repository;
            _unitOfWork = unitOfWork;
        }
        public void Borrar(Brand brand)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                repository.Borrar(brand);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool EstaRelacionado(Brand brand)
        {
            return repository.EstaRelacionado(brand);
        }

        public bool Existe(Brand brand)
        {
            return repository.Existe(brand);
        }

        public Brand? GetBrandPorId(int idEditar)
        {
            return repository.GetBrandPorId(idEditar);
        }

        public Brand GetBrandPorNombre(string brandN)
        {
            return repository.GetBrandPorNombre(brandN);
		}

		public int GetCantidad()
		{
			try
			{
				return repository.GetCantidad();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public List<Brand> GetLista()
        {
            return repository.GetLista();
        }

		public List<Brand> GetListaOrdenada(Orden orden)
		{
			try
			{
				return repository.GetListaOrdenada(orden);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public List<Brand> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			return repository.GetListaPaginada(page, pageSize, orden);
		}

		public List<Shoe>? GetShoes(Brand brand)
		{
            return repository.GetShoe(brand);
		}

		public void Guardar(Brand brand)
        {
            try
            {
                if (brand.BrandId==0)
                {
                    repository.Guardar(brand);
                }
                else
                {
                    repository.Editar(brand);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
