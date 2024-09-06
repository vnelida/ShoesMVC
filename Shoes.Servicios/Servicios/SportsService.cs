using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class SportsService : ISportsService
    {
        private readonly ISportsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

		public SportsService(ISportsRepository repository, IUnitOfWork unitOfWork)
        {
			_repository = repository ?? throw new ArgumentNullException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException("Dependencies not set");
		}
		public void Delete(Sport sport)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(sport);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool IsRelated(int id)
		{
			return _repository!.IsRelated(id);
		}

		public bool Exist(Sport sport)
		{
			return _repository!.Exist(sport);
		}

		public Sport? Get(Expression<Func<Sport, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Sport> GetAll(Expression<Func<Sport, bool>>? filter = null, Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public void Save(Sport sport)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (sport.SportId == 0)
				{
					_repository?.Add(sport);
				}
				else
				{
					_repository?.Update(sport);
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
