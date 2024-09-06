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
    public class BrandsService : IBrandsService
    {
        private readonly IBrandsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public BrandsService(IBrandsRepository repository,  IUnitOfWork unitOfWork)
        {
			_repository = repository ?? throw new ArgumentNullException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException("Dependencies not set");
		}

		public void Delete(Brand brand)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(brand);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool EstaRelacionado(int id)
		{
			return _repository!.EstaRelacionado(id);
		}

		public bool Existe(Brand brand)
        {
            return _repository!.Existe(brand);
        }

		public Brand? Get(Expression<Func<Brand, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Brand> GetAll(Expression<Func<Brand, bool>>? filter = null, Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public void Save(Brand brand)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (brand.BrandId == 0)
				{
					_repository?.Add(brand);
				}
				else
				{
					_repository?.Editar(brand);
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
