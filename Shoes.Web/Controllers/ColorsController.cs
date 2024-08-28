using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Color;
using X.PagedList;

namespace Shoes.Web.Controllers
{
	public class ColorsController : Controller
	{
		private readonly IColorsService? _services;
		private readonly IMapper _mapper;
		public ColorsController(IColorsService? services, IMapper mapper)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
		}
		public IActionResult Index(int? page)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			var colors = _services?.GetLista().ToPagedList(pageNumber, pageSize);
			return View(colors);
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
				Color? color = _services?.GetColorPorId(id.Value);
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
				_services.Guardar(color);
				TempData["success"] = "Record added/edited successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{

				ModelState.AddModelError(string.Empty, ex.Message);
				return View(colorVm);
			}
		}

		public IActionResult Delete(int? id)
		{
			if (id is null || id == 0)
			{
				return NotFound();
			}
			Color? color = _services?.GetColorPorId(id.Value);
			if (color is null)
			{
				return NotFound();
			}
			return View(color);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			Color? color = _services?.GetColorPorId(id);
			if (color is null)
			{
				return NotFound();
			}
			if (_services.EstaRelacionado(color))
			{
				ModelState.AddModelError(string.Empty, "Record is associated with other and cannot be deleted ");
				return View(color);
			}
			_services.Borrar(color);
			TempData["success"] = "Record deleted successfully";
			return RedirectToAction("Index");
		}

	}
}
