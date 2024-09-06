using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace Shoes.Web.Controllers
{
	public class ShoesController : Controller
	{
		private readonly IShoesService? _service;
		private readonly IBrandsService? _brandsService;
		private readonly IColorsService? _colorService;
		private readonly ISportsService? _sportService;
		private readonly IGenresService? _genreService;
		private readonly IMapper? _mapper;

		public ShoesController(IShoesService? service, IMapper? mapper, IBrandsService brandsService, IColorsService colorService, ISportsService sportService, IGenresService genreService)
		{
			_service = service ?? throw new ArgumentNullException(nameof(_service));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(_service));
			_brandsService = brandsService;
			_colorService = colorService;
			_sportService = sportService;
			_genreService = genreService;
		}

		public IActionResult Index(int? page = null)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			var shoes = _service?.GetAll(orderBy: o => o.OrderBy(b => b.Model), propertiesNames: "Brand,ColorN,Sport,Genre");
			var shoesVm = _mapper?.Map<List<ShoeListVm>>(shoes);
			return View(shoesVm!.ToPagedList(pageNumber, pageSize));
		}
		public IActionResult Upsert(int? id)
		{
			ShoeEditVm shoeVm;

			if (id == null || id == 0)
			{
				shoeVm = new ShoeEditVm();
				shoeVm.Brands = _brandsService!
					.GetAll(orderBy: q => q.OrderBy(c => c.BrandName))
					.Select(c => new SelectListItem
					{
						Text = c.BrandName,
						Value = c.BrandId.ToString()
					}).ToList();

				shoeVm.Colors = _colorService!
					 .GetAll(orderBy: q => q.OrderBy(c => c.ColorName))
					 .Select(c => new SelectListItem
					 {
						 Text = c.ColorName,
						 Value = c.ColorId.ToString()
					 }).ToList();

				shoeVm.Sports = _sportService!
						.GetAll(orderBy: q => q.OrderBy(c => c.SportName))
						.Select(c => new SelectListItem
						{
							Text = c.SportName,
							Value = c.SportId.ToString()
						}).ToList();

				shoeVm.Genres = _genreService!
						.GetAll(orderBy: q => q.OrderBy(c => c.GenreName))
						.Select(c => new SelectListItem
						{
							Text = c.GenreName,
							Value = c.GenreId.ToString()
						}).ToList();
				return View(shoeVm);


			}
			else
			{
				try
				{
					Shoe? shoe = _service!.Get(filter: c => c.ShoeId == id);
					if (shoe == null)
					{
						return NotFound();
					}
					shoeVm = _mapper!.Map<ShoeEditVm>(shoe);
					shoeVm.Brands =
						_brandsService!
						.GetAll(orderBy: o => o.OrderBy(c => c.BrandName))
						.Select(c => new SelectListItem
						{
							Text = c.BrandName,
							Value = c.BrandId.ToString()
						})
					.ToList();

					shoeVm.Colors =
						_colorService!
						.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))
						.Select(c => new SelectListItem
						{
							Text = c.ColorName,
							Value = c.ColorId.ToString()
						})
					.ToList();


					shoeVm.Sports =
						_sportService!
						.GetAll(orderBy: o => o.OrderBy(c => c.SportName))
						.Select(c => new SelectListItem
						{
							Text = c.SportName,
							Value = c.SportId.ToString()
						})
					.ToList();


					shoeVm.Genres =
						 _genreService!
						 .GetAll(orderBy: o => o.OrderBy(c => c.GenreName))
						 .Select(c => new SelectListItem
						 {
							 Text = c.GenreName,
							 Value = c.GenreId.ToString()
						 })
					 .ToList();


					return View(shoeVm);
				}
				catch (Exception)
				{

					throw;
				}
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpSert(ShoeEditVm shoeVm)
		{
			if (!ModelState.IsValid)
			{
				shoeVm.Brands =
					_brandsService!
					.GetAll(orderBy: o => o.OrderBy(c => c.BrandName))
					.Select(c => new SelectListItem
					{
						Text = c.BrandName,
						Value = c.BrandId.ToString()
					})
					.ToList();

				shoeVm.Colors =
					_colorService!
					.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))
					.Select(c => new SelectListItem
					{
						Text = c.ColorName,
						Value = c.ColorId.ToString()
					})
					.ToList();

				shoeVm.Sports =
					_sportService!
					.GetAll(orderBy: o => o.OrderBy(c => c.SportName))
					.Select(c => new SelectListItem
					{
						Text = c.SportName,
						Value = c.SportId.ToString()
					})
					.ToList();

				shoeVm.Genres =
					_genreService!
					.GetAll(orderBy: o => o.OrderBy(c => c.GenreName))
					.Select(c => new SelectListItem
					{
						Text = c.GenreName,
						Value = c.GenreId.ToString()
					})
					.ToList();

				return View(shoeVm);
			}

			if (_service == null || _mapper == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Dependencies are not properly configured.");
			}

			try
			{
				Shoe shoe = _mapper.Map<Shoe>(shoeVm);

				if (_service.Exist(shoe))
				{
					ModelState.AddModelError(string.Empty, "Record already exist");

					shoeVm.Brands =
						_brandsService!
						.GetAll(orderBy: o => o.OrderBy(c => c.BrandName))
						.Select(c => new SelectListItem
						{
							Text = c.BrandName,
							Value = c.BrandId.ToString()
						})
						.ToList();

					shoeVm.Colors =
						_colorService!
						.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))
						.Select(c => new SelectListItem
						{
							Text = c.ColorName,
							Value = c.ColorId.ToString()
						})
						.ToList();

					shoeVm.Sports =
						_sportService!
						.GetAll(orderBy: o => o.OrderBy(c => c.SportName))
						.Select(c => new SelectListItem
						{
							Text = c.SportName,
							Value = c.SportId.ToString()
						})
						.ToList();

					shoeVm.Genres =
						_genreService!
						.GetAll(orderBy: o => o.OrderBy(c => c.GenreName))
						.Select(c => new SelectListItem
						{
							Text = c.GenreName,
							Value = c.GenreId.ToString()
						})
						.ToList();

					return View(shoeVm);
				}

				_service.Save(shoe);
				TempData["success"] = "Record successfully added/edited";
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");

				shoeVm.Brands =
					_brandsService!
					.GetAll(orderBy: o => o.OrderBy(c => c.BrandName))
					.Select(c => new SelectListItem
					{
						Text = c.BrandName,
						Value = c.BrandId.ToString()
					})
					.ToList();

				shoeVm.Colors =
					_colorService!
					.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))
					.Select(c => new SelectListItem
					{
						Text = c.ColorName,
						Value = c.ColorId.ToString()
					})
					.ToList();

				shoeVm.Sports =
					_sportService!
					.GetAll(orderBy: o => o.OrderBy(c => c.SportName))
					.Select(c => new SelectListItem
					{
						Text = c.SportName,
						Value = c.SportId.ToString()
					})
					.ToList();

				shoeVm.Genres =
					_genreService!
					.GetAll(orderBy: o => o.OrderBy(c => c.GenreName))
					.Select(c => new SelectListItem
					{
						Text = c.GenreName,
						Value = c.GenreId.ToString()
					})
					.ToList();

				return View(shoeVm);
			}

		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			if (id is null || id == 0)
			{
				return NotFound();
			}
			Shoe? shoe = _service?.Get(filter: c => c.ShoeId == id);
			if (shoe is null)
			{
				return NotFound();
			}
			try
			{
				if (_service == null || _mapper == null)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, "Dependencies are not properly configured.");
				}

				if (_service.IsRelated(shoe.ShoeId))
				{
					return Json(new { success = false, message = "Related records found. Action denied." }); ;
				}
				_service.Delete(shoe);
				return Json(new { success = true, message = "Record successfully deleted." });
			}
			catch (Exception)
			{

				return Json(new { success = false, message = "Record could not be deleted." }); ;

			}
		}
	}
}
