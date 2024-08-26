using Microsoft.EntityFrameworkCore;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;
using System.Numerics;

namespace Shoes.Datos.Repositories
{
	public class ShoesRepository : IShoesRepository
	{
		private readonly ShoesDbContext context;
		public ShoesRepository(ShoesDbContext _context)
		{
			context = _context;
		}
		public void Agregar(Shoe shoe)
		{
			var brandExistente = context
				.Brands.FirstOrDefault(b => b.BrandId == shoe.BrandId);
			if (brandExistente != null)
			{
				context.Attach(brandExistente);
				shoe.Brand = brandExistente;
			}

			var genreExistente = context
				.Genres.FirstOrDefault(g => g.GenreId == shoe.GenreId);
			if (genreExistente != null)
			{
				context.Attach(genreExistente);
				shoe.Genre = genreExistente;
			}

			var colorExistente = context
				.Colors.FirstOrDefault(c => c.ColorId == shoe.ColorId);
			if (colorExistente != null)
			{
				context.Attach(colorExistente);
				shoe.ColorN = colorExistente;
			}

			var sportExistente = context
				.Sports.FirstOrDefault(s => s.SportiD == shoe.SportId);
			if (sportExistente != null)
			{
				context.Attach(sportExistente);
				shoe.Sport = sportExistente;
			}

			context.Shoes.Add(shoe);
		}

		public void AgregarSizeAShoe(Shoe shoe, List<Size> sizes)
		{
			foreach (var size in sizes)
			{
				var sizeExistente = context.Sizes.FirstOrDefault(s => s.SizeId == size.SizeId);

				if (sizeExistente == null)
				{
					context.Sizes.Add(size);
					sizeExistente = size;
				}
				else
				{
					context.Sizes.Attach(sizeExistente);

					if (!ExisteRelacion(shoe, sizeExistente))
					{
						context.ShoeSizes.Add(new ShoeSize
						{
							ShoeId = shoe.ShoeId,
							SizeId = sizeExistente.SizeId
						});
					}
				}

			}
		}
		public void AgregarShoeSize(ShoeSize nuevaRelacion)
		{
			context.Set<ShoeSize>().Add(nuevaRelacion);
		}

		public void Borrar(Shoe shoe)
		{
			context.Shoes.Remove(shoe);
		}

		public void Editar(Shoe shoe)
		{

			var colorExistente = context.Colors
				.FirstOrDefault(s => s.ColorId == shoe.ColorId);

			if (colorExistente != null)
			{
				context.Attach(colorExistente);
				shoe.ColorN = colorExistente;
			}

			var genreExistente = context
				.Genres.FirstOrDefault(g => g.GenreId == shoe.GenreId);
			if (genreExistente != null)
			{
				context.Attach(genreExistente);
				shoe.Genre = genreExistente;
			}

			var sportExistente = context
				.Sports.FirstOrDefault(s => s.SportiD == shoe.ShoeId);
			if (sportExistente != null)
			{
				context.Attach(sportExistente);
				shoe.Sport = sportExistente;
			}

			var brandExistente = context
				.Brands.FirstOrDefault(b => b.BrandId == shoe.BrandId);
			if (brandExistente != null)
			{
				context.Attach(brandExistente);
				shoe.Brand = brandExistente;
			}


			context.Shoes.Update(shoe);
		}

		public void Editar(Shoe shoe, int? sizeId = null)
		{
			context.Shoes.Update(shoe);
		}

		public bool Existe(Shoe shoe)
		{

			if (shoe.ShoeId == 0)
			{
				return context.Shoes.Any(
					p => p.Model == shoe.Model &&
					p.Description == shoe.Description &&
					p.ColorId == shoe.ColorId &&
					p.GenreId == shoe.GenreId &&
					p.SportId == shoe.SportId &&
					p.BrandId == shoe.BrandId);

			}
			return context.Shoes.Any(
				p => p.Model == shoe.Model &&
				p.Description == shoe.Description &&
				p.SportId == shoe.SportId &&
				p.ColorId == shoe.ColorId &&
				p.SportId == shoe.SportId &&
				p.GenreId == shoe.GenreId &&
				p.ShoeId != shoe.ShoeId);

		}

		public bool ExisteRelacion(Shoe shoe, Size size)
		{
			if (shoe == null || size == null) return false;

			var existeRelacion=context.Shoes.Include(ss=>ss.ShoesSizes)
											.ThenInclude(ss=>ss.SizeN)
											.Any(s=>s.ShoeId== shoe.ShoeId && s.ShoesSizes.Any(ss=>ss.SizeId==size.SizeId));
			
			return existeRelacion;
		}

		public int GetCantidad(Func<Shoe, bool>? filtro)
		{
			if (filtro != null)
			{
				return context.Shoes.Count(filtro);
			}
			else
			{
				return context.Shoes.Count();
			}
		}

		public List<Shoe> GetLista()
		{
			return context.Shoes
				.Include(s => s.ColorN)
				.Include(s => s.Genre)
				.Include(s => s.Brand)
				.Include(s => s.Sport)
				.ToList();
		}

		public List<ShoeListDto> GetListaDto()
		{
			return context.Shoes
				.Include(s => s.ColorN)
				.Include(s => s.Genre)
				.Include(s => s.Brand)
				.Include(s => s.Sport)
				.Select(n => new ShoeListDto
				{
					ShoeId = n.ShoeId,
					Model = n.Model,
					Description = n.Description,
					Genre = n.Genre != null ? n.Genre.GnereName : string.Empty,
					ColorN = n.ColorN != null ? n.ColorN.ColorName : string.Empty,
					Sport = n.Sport != null ? n.Sport.SportName : string.Empty,
					Brand = n.Brand != null ? n.Brand.BrandName : string.Empty,
					Price = n.Price
				}).ToList();
		}

		public List<ShoeListDto> GetListaPaginadaOrdenadaFiltrada(int page, int pageSize, Orden? orden, Brand? brandFiltro, Color? colorFiltro, Genre? genreFiltro, Sport? sportFiltro)
		{
			IQueryable<Shoe> query = context.Shoes
				.Include(p => p.Brand)
				.Include(p => p.ColorN)
				.Include(p => p.Genre)
				.Include(p => p.Sport)
				.Include(p=>p.ShoesSizes)
				.AsNoTracking();

			if (brandFiltro != null)
			{
				query = query
					.Where(p => p.BrandId == brandFiltro.BrandId);
			}

		
			if (colorFiltro != null)
			{
				query = query
					.Where(p => p.ColorId == colorFiltro.ColorId);
			}
			if (genreFiltro != null)
			{
				query = query
					.Where(p => p.GenreId == genreFiltro.GenreId);
			}

			
			if (sportFiltro != null)
			{
				query = query
					.Where(p => p.SportId == sportFiltro.SportiD);
			}

			
			if (orden != null)
			{
				switch (orden)
				{
					case Orden.AZ:
						query = query.OrderBy(p => p.Model);
						break;
					case Orden.ZA:
						query = query.OrderByDescending(p => p.Model);
						break;
					case Orden.MenorPrecio:
						query = query.OrderBy(p => p.Price);
						break;
					case Orden.MayorPrecio:
						query = query.OrderByDescending(p => p.Price);
						break;
					default:
						break;
				}
			}

		
			List<Shoe> listaPaginada = query
				.AsNoTracking()
				.Skip(page * pageSize)
				.Take(pageSize)
				.ToList();

			
			List<ShoeListDto> listaDto = listaPaginada
				.Select(p => new ShoeListDto
				{
					ShoeId = p.ShoeId,
					Model = p.Model,
					Description = p.Description,
					Price = p.Price,
					Brand = p.Brand?.BrandName,
					ColorN = p.ColorN?.ColorName,
					Genre = p.Genre.GnereName,
					Sport = p.Sport.SportName,
					CantidadDeTalles=p.ShoesSizes.Count(),
					
				})
				.ToList();

			return listaDto;
		}

		public Shoe? GetShoePorId(int shoeId)
		{
			return context.Shoes.Include(s => s.ColorN)
				.Include(s => s.Genre)
				.Include(s => s.Sport)
				.Include(s => s.Brand)
				.FirstOrDefault(s => s.ShoeId == shoeId);
		}

		public List<ShoeListDto> GetShoesSinTallle()
		{
			return context.Shoes.
				Include(s => s.ColorN)
				.Include(s => s.Genre)
				.Include(s => s.Sport)
				.Include(s => s.Brand)
				.Where (s =>!s.ShoesSizes.Any())
				.Select(s => new ShoeListDto
				{
					ShoeId = s.ShoeId,
					Model = s.Model,
					Description = s.Description,
					Genre = s.Genre != null ? s.Genre.GnereName : string.Empty,
					ColorN = s.ColorN != null ? s.ColorN.ColorName : string.Empty,
					Sport = s.Sport != null ? s.Sport.SportName : string.Empty,
					Brand = s.Brand != null ? s.Brand.BrandName : string.Empty,
					Price = s.Price
				}).ToList();
		}

		public List<Size>? GetSizePorShoes(int shoeId)
		{
			return context.ShoeSizes.Include(ss => ss.SizeN)
				.Where(ss => ss.ShoeId == shoeId)
				.Select(ss => ss.SizeN).ToList();
		}


		public List<SizeDetailDto>? GetSizeDetail(int shoeId)
		{

			return context.ShoeSizes.Include(ss => ss.SizeN)
			.Where(ss => ss.ShoeId == shoeId)
			.Select(ss => new SizeDetailDto
			{
				SizeId = ss.ShoeSizeId,
				SizeN = ss.SizeN.SizeNumber,
				Quantity = ss.QuantityInStock
			}).ToList();
		}


		public void EliminarRelacion(Shoe shoe)
		{
			var relacionesPasadas = context.ShoeSizes.Where(ss => ss.ShoeId == shoe.ShoeId).ToList();
			context.ShoeSizes.RemoveRange(relacionesPasadas);
		}

		public IEnumerable<IGrouping<int, Shoe>> GetShoesXGenre()
		{
			return context.Shoes.GroupBy(s => s.GenreId).ToList();
		}

		public IEnumerable<IGrouping<int, Shoe>> GetShoesXMarca()
		{
			return context.Shoes.GroupBy(s => s.BrandId).ToList();
		}


		public void AgregarStock(int shoeId, int sizeId, int unidades)
		{

			var shoeSize = GetShoeSize(shoeId, sizeId);
			if (shoeSize!=null)
			{
				shoeSize.QuantityInStock += unidades;
				context.ShoeSizes.Update(shoeSize);
			}
			
		}

		public ShoeSize GetShoeSize(int shoeId, int sizeId)
		{
			return context.ShoeSizes.FirstOrDefault(ss => ss.ShoeId == shoeId && ss.SizeId == sizeId);
		}

		public List<ShoeSize> GetListaShoesSizes(int shoeId)
		{
			return context.ShoeSizes
			.Where(ss => ss.ShoeId == shoeId)
			.Include(ss => ss.SizeN) 
			.ToList();
		}

		public void EditarSs(int shoeSizeId, int stock)
		{
			if (shoeSizeId == null)
			{
				throw new ArgumentNullException(nameof(shoeSizeId));
			}

			var existingShoeSize = context.ShoeSizes
				.FirstOrDefault(ss => ss.ShoeSizeId == shoeSizeId);

			if (existingShoeSize != null)
			{
				existingShoeSize.QuantityInStock =stock;
				context.SaveChanges();
			}
			else
			{
				throw new InvalidOperationException("El ShoeSize no existe.");
			}
		}
	}
}
