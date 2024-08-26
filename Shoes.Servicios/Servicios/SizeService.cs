using Shoes.Datos.Interfaces;
using Shoes.Datos;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
	public class SizeService : ISizeService
	{
		private readonly ISizeRepository repository;
		private readonly IUnitOfWork _unitOfWork;

		public SizeService(ISizeRepository _repository, IUnitOfWork unitOfWork )
        {
			repository = _repository;
			_unitOfWork = unitOfWork;
		}
        public void Borrar(Size size)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				repository.Borrar(size);
				_unitOfWork.Commit();
			}
			catch (Exception)
			{
				_unitOfWork?.Rollback();
				throw;
			}
		}

		public bool EstaRelacionado(Size size)
		{
			return repository.EstaRelacionado(size);
		}

		public bool Existe(Size size)
		{
			return repository.Existe(size);
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

		public List<Size> GetLista()
		{
			return repository.GetLista();
		}

		public List<Size> GetListaPaginada(int page, int pageSize)
		{
			return repository.GetListaPaginada(page, pageSize);
		}

		public List<Shoe> GetShoes(Size? sizeEnDB)
		{
			return repository.GetShoe(sizeEnDB);
		}

		public Size? GetSizePorId(int id)
		{
			return repository.GetSizePorId(id);
		}

		public Size? GetSizePorIDxShoe(int id, bool incluyeShoe = false)
		{
			return repository.GetSizePorIDxShoe(id, incluyeShoe);
		}

		public Size GetSizePorNombre(string sizeN)
		{
			return repository.GetSizetPorNombre(sizeN);
		}

		public void Guardar(Size size)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				if (size.SizeId == 0)
				{
					repository.Agregar(size);
				}
				else
				{
					repository.Editar(size);
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
