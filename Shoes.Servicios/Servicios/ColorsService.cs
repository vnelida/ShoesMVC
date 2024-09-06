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
    public class ColorsService : IColorsService
    {
        private readonly IColorsRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public ColorsService(IColorsRepository repository, IUnitOfWork unitOfWork)
        {
			_repository = repository ?? throw new ArgumentNullException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException("Dependencies not set");
		}

		public void Delete(Color color)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(color);
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

		public bool Existe(Color color)
		{
			return _repository!.Existe(color);
		}

		public Color? Get(Expression<Func<Color, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Color> GetAll(Expression<Func<Color, bool>>? filter = null, Func<IQueryable<Color>, IOrderedQueryable<Color>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		

		public void Save(Color color)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (color.ColorId == 0)
				{
					_repository?.Add(color);
				}
				else
				{
					_repository?.Editar(color);
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
