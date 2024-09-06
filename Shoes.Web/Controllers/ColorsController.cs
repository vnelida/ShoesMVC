using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Color;
using Shoes.Web.ViewModels.Colors;
using X.PagedList.Extensions;

namespace Shoes.Web.Controllers
{
	public class ColorsController : Controller
	{
		private readonly IColorsService? _services;
		private readonly IMapper _mapper;
		public ColorsController(IColorsService? services, IMapper mapper)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
		}
		public IActionResult Index(int? page)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			var colors = _services?.GetAll(orderBy: o => o.OrderBy(c => c.ColorName));
			var colorsVm=_mapper?.Map<List<ColorListVm>>(colors).ToPagedList(pageNumber, pageSize);
			return View(colorsVm);
		}
		public IActionResult UpSert(int? id)
		{
			if (_services is null || _mapper is null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
			}

			ColorEditVm colorVm;
			if (id is null || id == 0)
			{
				colorVm= new ColorEditVm();
			}
			else
			{
				Color? color = _services?.Get(filter: c => c.ColorId == id);
				if (color is null)
				{
					return NotFound();
				}
				colorVm = _mapper.Map<ColorEditVm>(color);
			}
			
			
			return View(colorVm);
		}
		[HttpPost]
		public IActionResult UpSert(ColorEditVm colorVm)
		{
			if (!ModelState.IsValid)
			{
				return View(colorVm);
			}
			if (_services is null || _mapper is null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
			}
			Color? color = _mapper.Map<Color>(colorVm);

			try
			{
				if (_services.Existe(color))
				{
					ModelState.AddModelError(string.Empty, "Color already exist");
					return View(colorVm);
				}
				_services.Save(color);
				TempData["success"] = "Record added/edited successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{

				ModelState.AddModelError(string.Empty, ex.Message);
				return View(colorVm);
			}
		}


		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			if (id is null || id == 0)
			{
				return NotFound();
			}
			Color? color = _services?.Get(filter: c => c.ColorId == id);
			if (color is null)
			{
				return NotFound();
			}

			try
			{
				if (_services is null || _mapper is null)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
				}
				if (_services.EstaRelacionado(color.ColorId))
				{
					return Json(new { success = false, message = "Related record. Delete denny" });
				}
				_services.Delete(color);
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
			Color? color = _services?.Get(filter: c => c.ColorId == id);
			if (color is null)
			{
				return NotFound();
			}
			if (_services.EstaRelacionado(color.ColorId))
			{
				ModelState.AddModelError(string.Empty, "Record is associated with other and cannot be deleted ");
				return View(color);
			}
			_services.Delete(color);
			TempData["success"] = "Record deleted successfully";
			return RedirectToAction("Index");
		}

	}
}
