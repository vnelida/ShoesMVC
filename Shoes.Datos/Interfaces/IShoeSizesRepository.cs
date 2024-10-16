using Shoes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Datos.Interfaces
{
    public interface IShoeSizesRepository:IGenericRepository<ShoeSize>
    {
        void Update(ShoeSize shoeSize);
        bool Exist(ShoeSize shoeSize);
        bool IsRelated(int id);
    }
}
