using Shoes.Datos.Interfaces;
using Shoes.Datos;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Shoes.Servicios.Servicios
{
	public class SizeService : ISizeService
	{
		private readonly ISizeRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public SizeService(ISizeRepository repository, IUnitOfWork unitOfWork )
        {
			_repository = repository ?? throw new ArgumentNullException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException("Dependencies not set");
		}
	

		public void Delete(Size size)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(size);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Size size)
		{
			return _repository.Exist(size);
		}


		public Size? Get(Expression<Func<Size, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Size> GetAll(Expression<Func<Size, bool>>? filter = null, Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}
		public bool IsRelated(int id)
		{
			return _repository.IsRelated(id);
		}

		public void Save(Size size)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (size.SizeId == 0)
				{
					_repository?.Add(size);
				}
				else
				{
					_repository?.Update(size);
				}
				_unitOfWork?.Commit();

			}
			catch (Exception)
			{
				_unitOfWork?.Rollback();
				throw;
			}
		}
	}
}
