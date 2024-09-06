using Microsoft.EntityFrameworkCore;
using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class ShoesService : IShoesService
    {
        private readonly IShoesRepository _repository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;

		public ShoesService(IShoesRepository repository, IUnitOfWork unitOfWork, ISizeRepository sizeRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _sizeRepository = sizeRepository;
        }

		public void Delete(Shoe shoe)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				_repository?.Delete(shoe);
				_unitOfWork?.Commit();

			}
			catch (Exception)
			{
				_unitOfWork?.Rollback();
				throw;
			}
		}

		public bool Exist(Shoe shoe)
		{
			if (_repository is null)
			{
				throw new ApplicationException("Dependency not set");
			}
			return _repository.Exist(shoe);
		}

		public Shoe? Get(Expression<Func<Shoe, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			if (_repository == null)
			{
				throw new ApplicationException("Dependency not set");
			}

			return _repository.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null, Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null, string? propertiesNames = null)
		{
			if (_repository == null)
			{
				throw new ApplicationException("Dependency not set");
			}
			return _repository.GetAll(filter, orderBy, propertiesNames);
		}

		public bool IsRelated(int id)
		{
			return _repository.IsRelated(id);
		}

		public void Save(Shoe shoe)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (shoe.ShoeId == 0)
				{
					_repository?.Add(shoe);
				}
				else
				{
					_repository?.Update(shoe);
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
