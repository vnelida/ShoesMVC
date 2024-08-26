using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entidades
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GnereName { get; set; } = null!;
        public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
}
