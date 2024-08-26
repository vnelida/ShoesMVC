using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entidades.Dto
{
    public class ShoeListDto
    {
        public int ShoeId;

        public string Model=null!;

        public string Description = null!;

        public decimal Price;

        public string Brand = null!;

        public string Genre = null!;

        public string Sport = null!;

        public string ColorN = null!;
        public int CantidadDeTalles { get; set; }
    }
}
