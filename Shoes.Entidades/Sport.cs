using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entidades
{
    public class Sport
    {

        public int SportiD { get; set; }
        public string SportName { get; set; } = null!;
        public ICollection<Shoe> Shoes { get; set; } =new List<Shoe>();
    }
}
