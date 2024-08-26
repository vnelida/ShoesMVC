using Microsoft.EntityFrameworkCore;
using Shoes.Datos;
using Shoes.Datos.Interfaces;
using Shoes.Entidades;
using Shoes.Entidades.Dto;
using Shoes.Entidades.Enums;
using Shoes.Servicios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Servicios.Servicios
{
    public class ShoesService : IShoesService
    {
        private readonly IShoesRepository repository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;

		public ShoesService(IShoesRepository _repository, IUnitOfWork unitOfWork, ISizeRepository sizeRepository)
        {
             repository = _repository;
            _unitOfWork = unitOfWork;
            _sizeRepository = sizeRepository;
        }

		public void AgregarStock(int shoeId, int sizeId, int unidades)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				var sShoe = repository.GetShoePorId(shoeId);
				var sSize=_sizeRepository.GetSizePorId(sizeId);
				if (sSize != null || sShoe != null)
				{
					repository.AgregarStock(shoeId, sizeId, unidades);
					_unitOfWork.SaveChanges();
				}

				_unitOfWork.Commit();
			}
			catch (Exception ex) 
			{
				_unitOfWork.Rollback();
				throw new Exception("Error al agregar stock.", ex);
			}
		}

		public void AsignarSizeAShoe(Shoe shoe, Size size)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				if (size.SizeId==0)
				{
					_sizeRepository.Agregar(size);
					_unitOfWork.SaveChanges();
				}
				if (shoe.ShoeId == 0)
				{
					repository.Agregar(shoe);
					_unitOfWork.SaveChanges();
				}
				ShoeSize nuevaRelacion = new ShoeSize
				{
					ShoeId = shoe.ShoeId,
					SizeId = size.SizeId,
					QuantityInStock = 0
				};

				repository.AgregarShoeSize(nuevaRelacion);
				_unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new Exception("Error al asignar.", ex);
				
			}
		}

		public void Borrar(Shoe shoe)
        {
            try
            {
                _unitOfWork.BeginTransaction();
				if (shoe.ShoesSizes!=null)
				{
					foreach (var shoeSize in shoe.ShoesSizes.ToList())
					{
						shoe.ShoesSizes.Remove(shoeSize);
					}
				}
				
				repository.Borrar(shoe);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
				_unitOfWork?.Rollback();
				throw;
            }

        }

		public void Editar(Shoe shoe, int? sizeId = null)
		{
			try
			{
				_unitOfWork.BeginTransaction();

				repository.Editar(shoe);
				if (sizeId.HasValue)
				{
					var size = _sizeRepository.GetSizePorId(sizeId.Value);
					if (size!=null)
					{
						if (!shoe.ShoesSizes.Any(ss => ss.SizeId == sizeId))
						{
							var nuevaRelacion = new ShoeSize
							{
								ShoeId = shoe.ShoeId,
								SizeId = size.SizeId,
								QuantityInStock = 1
							}; repository.AgregarShoeSize(nuevaRelacion);
						}
					}
					else
					{
						throw new Exception("Talle no encontrado.");
					}
				
				}
				_unitOfWork.Commit();

			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public void EditarSs(int shoeSizeId, int stock)
		{
			repository.EditarSs(shoeSizeId, stock);
		}

		public bool Existe(Shoe shoe)
        {
            return repository.Existe(shoe);
        }

		public bool ExisteRelacion(Shoe shoe, Size size)
		{
			return repository.ExisteRelacion(shoe, size);
		}

		public int GetCantidad(Func<Shoe, bool>? filtro = null)
        {
            return repository.GetCantidad(filtro);
        }

        public List<Shoe> GetLista()
        {
            return repository.GetLista();
        }

        public IEnumerable<object> GetListaAnonima()
        {
            throw new NotImplementedException();
        }

        public List<ShoeListDto> GetListaDto()
        {
            return repository.GetListaDto();
        }

		public List<ShoeListDto> GetListaPaginadaOrdenadaFiltrada(int page, int pageSize, Orden? orden = null, Brand? brandFiltro = null, Color? colorFiltro = null, Genre? genreFiltro = null, Sport? sportFiltro = null)
		{
			return repository.GetListaPaginadaOrdenadaFiltrada(page, pageSize,
			   orden, brandFiltro, colorFiltro, genreFiltro, sportFiltro);
		}

		public List<ShoeSize> GetListaShoesSizes(int shoeId)
		{
			return repository.GetListaShoesSizes(shoeId);
		}

		public Shoe? GetShoePorId(int shoeId)
        {
            return repository.GetShoePorId(shoeId);
        }

		public ShoeSize GetShoeSize(int shoeId, int sizeId)
		{
			return repository.GetShoeSize(shoeId, sizeId);
		}

		public IEnumerable<IGrouping<int, Shoe>> GetShoesXBrands()
		{
			return repository.GetShoesXMarca();
		}

		public IEnumerable<IGrouping<int, Shoe>> GetShoesXGenre()
		{
			return repository.GetShoesXGenre();
		}

		public List<SizeDetailDto>? GetSizeDetalle(int shoeId)
		{
			return repository.GetSizeDetail(shoeId);
		}

		public List<Size>? GetSizePorShoes(int shoeId)
		{
			return repository.GetSizePorShoes(shoeId);
		}

		public void Guardar(Shoe shoe, List<Size>? sizes=null)
        {
            try
            {
                _unitOfWork.BeginTransaction();
				if (shoe.ShoeId == 0)
				{
					repository.Agregar(shoe);
					_unitOfWork.SaveChanges();
					if (sizes != null && sizes.Any())
					{
						repository.AgregarSizeAShoe(shoe, sizes);
					}
				}
				else
				{
					repository.Editar(shoe);
					_unitOfWork.SaveChanges();

					if (sizes != null)
					{
						repository.EliminarRelacion(shoe);
						_unitOfWork.SaveChanges();

						if (sizes.Any())
						{
							repository.AgregarSizeAShoe(shoe, sizes);
						}
					}
				}
				_unitOfWork.SaveChanges();
				_unitOfWork.Commit();
			}
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
            
        }

		public void GuardarConTalle(Shoe shoe, Size size)
		{
			try
			{
				_unitOfWork.BeginTransaction();

				repository.Agregar(shoe);

				if (size.SizeId==0)
				{
					_sizeRepository.Agregar(size);

				}


				_unitOfWork.Commit();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public List<ShoeListDto> ShoesSinTalle()
		{
			//revisar dto por las dudassss
			return repository.GetShoesSinTallle();
		}
	}
}
