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
    public class ColorsService : IColorsService
    {
        private readonly IColorsRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public ColorsService(IColorsRepository repository, IUnitOfWork unitOfWork)
        {
                _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Borrar(Color color)
        {
            try
            {
				_unitOfWork.BeginTransaction();
				_repository.Borrar(color);
				_unitOfWork.Commit();
			}
            catch (Exception)
            {
				_unitOfWork?.Rollback();
				throw;
            }
        }

        public bool EstaRelacionado(Color color)
        {
            return _repository.EstaRelacionado(color);
        }

        public bool Existe(Color color)
        {
            try
            {
                return _repository.Existe(color);
            }
            catch (Exception)
            {

                throw;
            }
        }

		public int GetCantidad()
		{
			try
			{
				return _repository.GetCantidad();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public Color? GetColorPorId(int idEditar)
        {
            try
            {
                return _repository.GetColorPorId(idEditar);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Color? GetColorPorNombre(string color)
        {
            return _repository.GetColorPorNombre(color);
        }

        public List<Color> GetLista()
        {
            return _repository.GetLista();
        }

		public List<Color> GetListaOrdenada(Orden orden)
		{
			try
			{
				return _repository.GetListaOrdenada(orden);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public List<Color> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			return _repository.GetListaPaginada(page, pageSize, orden);
		}

		public List<Shoe>? GetShoes(Color? colorEnDB)
		{
			return _repository.GetShoe(colorEnDB);
		}

		public void Guardar(Color color)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (color.ColorId == 0)
                {
                    _repository.Agregar(color);
                }
                else
                {
                    _repository.Editar(color);
                }
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
