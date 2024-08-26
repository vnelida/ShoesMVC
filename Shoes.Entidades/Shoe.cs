using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entidades
{
    public class Shoe
    {

        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int SportId { get; set; }
        public Sport? Sport { get; set; }
        public int ColorId { get; set; }
        public Color? ColorN { get; set; }

		public ICollection<ShoeSize> ShoesSizes { get; set; } = new List<ShoeSize>();


	}
}
