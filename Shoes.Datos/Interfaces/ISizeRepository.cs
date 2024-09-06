using Shoes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Datos.Interfaces
{
	public interface ISizeRepository:IGenericRepository<Size>
	{
		void Update(Size genre);
		bool Exist(Size genre);
		bool IsRelated(int id);
	}
}
