using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Genres;
using Shoes.Web.ViewModels.Sizes;
using Shoes.Web.ViewModels.Sports;
using System.Drawing;
using System.Security.Policy;
using X.PagedList.Extensions;
using Size = Shoes.Entidades.Size;

namespace Shoes.Web.Controllers
{
	public class SizesController : Controller
	{
		private readonly ISizeService? _sizeServices;
		private readonly IMapper? _mapper;
		public SizesController(ISizeService? sizeServices, IMapper mapper)
		{
			_sizeServices = sizeServices ?? throw new ArgumentNullException(nameof(sizeServices));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public IActionResult Index(int? page)
		{
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var size = _sizeServices?.GetAll(orderBy: o => o.OrderBy(c => c.SizeNumber));
            var sizeVm = _mapper?.Map<List<SizeListVm>>(size).ToPagedList(pageNumber, pageSize);
            return View(sizeVm);
        }
        public IActionResult UpSert(int? id)
        {
            SizeEditVm sizeVm;
            if (id is null || id == 0)
            {
                sizeVm = new SizeEditVm();
            }
            else
            {
                Size? size = _sizeServices?.Get(filter: c => c.SizeId == id);
                if (size is null)
                {
                    return NotFound();
                }
                sizeVm = _mapper.Map<SizeEditVm>(size);
            }


            return View(sizeVm);
        }
        [HttpPost]
        public IActionResult UpSert(SizeEditVm sizeVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sizeVm);
            }
            if (_sizeServices is null || _mapper is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
            }
            Size? size = _mapper.Map<Size>(sizeVm);

            try
            {
                if (_sizeServices.Exist(size))
                {
                    ModelState.AddModelError(string.Empty, "Item already exist");
                    return View(sizeVm);
                }
                _sizeServices.Save(size);
                TempData["success"] = "Record added/edited successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(size);
            }
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Size? size = _sizeServices?.Get(filter: c => c.SizeId == id);
            if (size is null)
            {
                return NotFound();
            }

            try
            {
                if (_sizeServices is null || _mapper is null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
                }
                if (_sizeServices.IsRelated(size.SizeId))
                {
                    return Json(new { success = false, message = "Related record. Delete denny" });
                }
                _sizeServices.Delete(size);
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
            Size? size = _sizeServices?.Get(filter: c => c.SizeId == id);
            if (size is null)
            {
                return NotFound();
            }
            if (_sizeServices.IsRelated(size.SizeId))
            {
                ModelState.AddModelError(string.Empty, "Record is associated with other and cannot be deleted ");
                return View(size);
            }
            _sizeServices.Delete(size);
            TempData["success"] = "Record deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
