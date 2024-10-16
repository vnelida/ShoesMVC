using Shoes.Datos.Interfaces;
using Shoes.Datos;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class ShoeSizeService : IShoeSizeService
    {
        private readonly IShoeSizesRepository _shoeSizesRepository;
        private readonly IShoesRepository _repository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShoeSizeService(IShoeSizesRepository shoeSizesRepository, IShoesRepository repository, ISizeRepository sizeRepository, IUnitOfWork unitOfWork)
        {
            _shoeSizesRepository = shoeSizesRepository;
            _repository = repository;
            _sizeRepository = sizeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Delete(ShoeSize shoeSize)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _shoeSizesRepository?.Delete(shoeSize);
                _unitOfWork?.Commit();
            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(ShoeSize shoeSize)
        {
            if (_shoeSizesRepository == null)
            {
                throw new ApplicationException("Dependency not set");
            }
            return _shoeSizesRepository.Exist(shoeSize);
        }

        public ShoeSize? Get(Expression<Func<ShoeSize, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
        {
            if (_shoeSizesRepository == null)
            {
                throw new ApplicationException("Dependency not set");
            }
            return _shoeSizesRepository.Get(filter, propertiesNames, tracked);
        }

        public IEnumerable<ShoeSize> GetAll(Expression<Func<ShoeSize, bool>>? filter = null, Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null, string? propertiesNames = null)
        {
            if (_shoeSizesRepository == null)
            {
                throw new ApplicationException("Dependency not set");
            }
            return _shoeSizesRepository.GetAll(filter, orderBy, propertiesNames);
        }

        public bool IsRelated(int id)
        {
            return _shoeSizesRepository.IsRelated(id);
        }

        public void Save(ShoeSize shoeSize)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (shoeSize.ShoeSizeId == 0)
                {
                    _shoeSizesRepository?.Add(shoeSize);
                }
                else
                {
                    _shoeSizesRepository?.Update(shoeSize);
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
