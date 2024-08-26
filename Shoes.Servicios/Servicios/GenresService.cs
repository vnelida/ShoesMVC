using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository repository;
        private readonly IUnitOfWork _unitOfWork;

		public GenresService(IGenresRepository _repository, IUnitOfWork unitOfWork)
        {
            repository = _repository;
            _unitOfWork = unitOfWork;
                
        }
        public void Borrar(Genre genre)
        {
			try
            {
				_unitOfWork.BeginTransaction();
				repository.Borrar(genre);
				_unitOfWork.Commit();
			}
            catch (Exception)
            {
				_unitOfWork?.Rollback();
				throw;
            }
        }

        public bool EstaRelacionado(Genre genre)
        {
            return repository.EstaRelacionado(genre);
        }

        public bool Existe(Genre genre)
        {
            return repository.Existe(genre);
        }

        public Genre? GetGenrePorId(int idEditar)
        {
            return repository.GetGenrePorId(idEditar);
        }

        public Genre GetGenrePorNombre(string genreN)
        {
            return repository.GetGenrePorNombre(genreN);
        }

        public List<Genre> GetLista()
        {
            return repository.GetLista();
        }

        public void Guardar(Genre genre)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (genre.GenreId == 0)
                {
                    repository.Guardar(genre);
                }
                else
                {
                    repository.Editar(genre);
                  
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

		public List<Genre> GetListaOrdenada(Orden orden)
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

		public List<Genre> GetListaPaginada(int page, int pageSize, Orden? orden = Orden.AZ)
		{
			return repository.GetListaPaginada(page, pageSize, orden);
		}

		public List<Shoe>? GetShoes(Genre genre)
		{
			return repository.GetShoe(genre);
		}
	}
}
