using Shoes.Entidades;
using System.Linq.Expressions;

namespace Shoes.Servicios.Interface
{
    public interface IGenresService
    {
        IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>>? filter = null, Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null, string? propertiesNames = null);
        Genre? Get(Expression<Func<Genre, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
        bool Exist(Genre genre);
        bool IsRelated(int id);
        void Save(Genre genre);
        void Delete(Genre genre);
    }
}
