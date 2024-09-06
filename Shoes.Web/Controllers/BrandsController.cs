using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Brands;
using X.PagedList.Extensions;

namespace Shoes.Web.Controllers
{
	public class BrandsController : Controller
	{
		private readonly IBrandsService? _services;
		private readonly IMapper? _mapper;
		public BrandsController(IBrandsService? services, IMapper mapper)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public IActionResult Index(int? page)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			var brands = _services?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName));
			var brandVm = _mapper?.Map<List<BrandListVm>>(brands).ToPagedList(pageNumber, pageSize);
			return View(brandVm);
		}
		public IActionResult Upsert(int? id)
		{
			if (_services is null || _mapper is null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
			}

			BrandEditVm brandVm;
			if (id is null || id == 0)
			{
				brandVm = new BrandEditVm();
			}
			else
			{
				Brand? brand = _services?.Get(filter: b => b.BrandId == id);
				if (brand is null)
				{
					return NotFound();
				}
				brandVm = _mapper.Map<BrandEditVm>(brand);
			}


			return View(brandVm);
		}
		[HttpPost]
		public IActionResult UpSert(BrandEditVm brandVm)
		{
			if (!ModelState.IsValid)
			{
				return View(brandVm);
			}
			if (_services is null || _mapper is null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
			}
			Brand? brand = _mapper.Map<Brand>(brandVm);

			try
			{
				if (_services.Existe(brand))
				{
					ModelState.AddModelError(string.Empty, "Brand already exist");
					return View(brandVm);
				}
				_services.Save(brand);
				TempData["success"] = "Record added/edited successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{

				ModelState.AddModelError(string.Empty, ex.Message);
				return View(brandVm);
			}
		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			if (id is null || id == 0)
			{
				return NotFound();
			}
			Brand? brand = _services?.Get(filter: b => b.BrandId == id);
			if (brand is null)
			{
				return NotFound();
			}

			try
			{
				if (_services is null || _mapper is null)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
				}
				if (_services.EstaRelacionado(brand.BrandId))
				{
					return Json(new { success = false, message = "Related record. Delete denny" });
				}
				_services.Delete(brand);
				return Json(new { success = true, message = "Record delete successfully." });

			}
			catch (Exception)
			{
				return Json(new { success = false, message = "Failed to delete the item." });

			}
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			Brand? brand = _services?.Get(filter: b => b.BrandId == id);
			if (brand is null)
			{
				return NotFound();
			}
			if (_services.EstaRelacionado(brand.BrandId))
			{
				ModelState.AddModelError(string.Empty, "Record is associated with others and cannot be deleted ");
				return View(brand);
			}
			_services.Delete(brand);
			TempData["success"] = "Record deleted successfully";
			return RedirectToAction("Index");
		}


	}
}
