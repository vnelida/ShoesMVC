using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Sports;
using X.PagedList.Extensions;

namespace Shoes.Web.Controllers
{
	public class SportsController : Controller
    {
        private readonly ISportsService? _services;
        private readonly IMapper? _mapper;

        public SportsController(ISportsService? services, IMapper? mapper)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var sport = _services?.GetAll(orderBy: o => o.OrderBy(c => c.SportName));
            var sportVm = _mapper?.Map<List<SportListVm>>(sport).ToPagedList(pageNumber, pageSize);
            return View(sportVm);
        }
		public IActionResult UpSert(int? id)
		{
			if (_services is null || _mapper is null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
			}

			SportsEditVm sportVm;
			if (id is null || id == 0)
			{
				sportVm = new SportsEditVm();
			}
			else
			{
				Sport? sport = _services?.Get(filter: c => c.SportId == id);
				if (sport is null)
				{
					return NotFound();
				}
				sportVm = _mapper.Map<SportsEditVm>(sport);
			}


			return View(sportVm);
		}
		[HttpPost]
		public IActionResult UpSert(SportsEditVm sportVm)
		{
			if (!ModelState.IsValid)
			{
				return View(sportVm);
			}
			if (_services is null || _mapper is null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
			}
			Sport? sport = _mapper.Map<Sport>(sportVm);

			try
			{
				if (_services.Exist(sport))
				{
					ModelState.AddModelError(string.Empty, "Item already exist");
					return View(sportVm);
				}
				_services.Save(sport);
				TempData["success"] = "Record added/edited successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{

				ModelState.AddModelError(string.Empty, ex.Message);
				return View(sport);
			}
		}


		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			if (id is null || id == 0)
			{
				return NotFound();
			}
			Sport? sport = _services?.Get(filter: c => c.SportId == id);
			if (sport is null)
			{
				return NotFound();
			}

			try
			{
				if (_services is null || _mapper is null)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
				}
				if (_services.IsRelated(sport.SportId))
				{
					return Json(new { success = false, message = "Related record. Delete denny" });
				}
				_services.Delete(sport);
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
			Sport? sport = _services?.Get(filter: c => c.SportId == id);
			if (sport is null)
			{
				return NotFound();
			}
			if (_services.IsRelated(sport.SportId))
			{
				ModelState.AddModelError(string.Empty, "Record is associated with other and cannot be deleted ");
				return View(sport);
			}
			_services.Delete(sport);
			TempData["success"] = "Record deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
