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
    public class SportsService : ISportsService
    {
        private readonly ISportsRepository repository;
        private readonly IUnitOfWork _unitOfWork;

		public SportsService(ISportsRepository _repository, IUnitOfWork unitOfWork)
        {
            repository = _repository;
            _unitOfWork = unitOfWork;
        }
        public void Borrar(Sport sport)
        {
			try
            {
				_unitOfWork.BeginTransaction();
				repository.Borrar(sport);
				_unitOfWork.Commit();
			}
            catch (Exception)
            {
				_unitOfWork?.Rollback();
				throw;
            }
        }

        public bool EstaRelacionado(Sport sport)
        {
            return repository.EstaRelacionado(sport);
        }

        public bool Existe(Sport sport)
        {
            return repository.Existe(sport);
        }

        public List<Sport> GetLista()
        {
            return repository.GetLista();
        }

        public Sport? GetSportPorId(int idEditar)
        {
            return repository.GetSportPorId(idEditar);
        }

        public Sport? GetSportPorNombre(string sportEdit)
        {
            return repository.GetSportPorNombre(sportEdit);
        }

        public void Guardar(Sport sport)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (sport.SportiD == 0)
                {
                    repository.Guardar(sport);
                }
                else
                {
                    repository.Editar(sport);
                }
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
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

		public List<Sport> GetListaOrdenada(Orden orden)
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

		public List<Sport> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			return repository.GetListaPaginada(page, pageSize, orden);
		}

		public List<Shoe>? GetShoes(Sport sport)
		{
			return repository.GetShoe(sport);
		}
	}
}
