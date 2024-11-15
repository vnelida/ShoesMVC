using Shoes.Entidades;
using System.Linq.Expressions;

namespace Shoes.Servicios.Interface
{
    public interface IShoeSizeService
    {
        IEnumerable<ShoeSize> GetAll(Expression<Func<ShoeSize, bool>>? filter = null, Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null, string? propertiesNames = null);
        ShoeSize? Get(Expression<Func<ShoeSize, bool>>? filter = null, string? propertiesNames = null, bool tracked = true);
        bool Exist(ShoeSize shoeSize);
        bool IsRelated(int id);
        void Save(ShoeSize shoeSize);
        void Delete(ShoeSize shoeSize);
    }
}
