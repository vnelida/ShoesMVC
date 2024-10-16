using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Datos.Repositories
{
    public class ShoeSizesRepository :GenericRepository<ShoeSize>, IShoeSizesRepository
    {
        private readonly ShoesDbContext _context;

        public ShoeSizesRepository(ShoesDbContext context):base(context)
        {
            _context = context;
        }

        public bool Exist(ShoeSize shoeSize)
        {
            return _context.ShoeSizes.Any(
            s => s.ShoeId == shoeSize.ShoeId && s.SizeId == shoeSize.SizeId);
        }

        public bool IsRelated(int id)
        {
            var existeRelacion = _context.ShoeSizes.Any(s => s.ShoeSizeId == id);

            return existeRelacion;
        }

        public void Update(ShoeSize shoeSize)
        {
            _context.ShoeSizes.Update(shoeSize);
        }
    }
}
