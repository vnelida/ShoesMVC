using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entidades
{
	public class ShoeSize
	{
        public int ShoeSizeId { get; set; }
        public int ShoeId { get; set; }
		public Shoe ShoeN { get; set; } = null;
		public int SizeId { get; set; }
		public Size SizeN { get; set; } = null;
		public int QuantityInStock { get; set; }
    }
}
