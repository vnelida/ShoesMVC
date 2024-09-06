using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Enums;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

		public GenresService(IGenresRepository repository, IUnitOfWork unitOfWork)
        {
			_repository = repository ?? throw new ArgumentNullException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException("Dependencies not set");
        }
		public void Delete(Genre genre)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(genre);
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

		public bool Exist(Genre genre)
		{
			return _repository!.Exist(genre);
		}

		public Genre? Get(Expression<Func<Genre, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>>? filter = null, Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public void Save(Genre genre)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (genre.GenreId == 0)
				{
					_repository?.Add(genre);
				}
				else
				{
					_repository?.Update(genre);
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
