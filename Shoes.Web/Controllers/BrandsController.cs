using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Entidades.ViewModels.Brand;
using Shoes.Servicios.Interface;
using X.PagedList;

namespace Shoes.Web.Controllers
{
	public class BrandsController : Controller
	{
		private readonly IBrandsService? _services;
        public BrandsController(IBrandsService? service)
        {
			_services = service;	
        }
        public IActionResult Index(int? page)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			var listBarnds = _services?.GetLista().ToPagedList(pageNumber, pageSize); ;
			return View(listBarnds);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(BrandEditVm brandVm)
		{
			if (!ModelState.IsValid)
			{
				return View(brandVm);
			}
			Brand brand = new Brand
			{
				BrandId = brandVm.BrandId,
				BrandName = brandVm.BrandName ?? string.Empty,

			}; 
			if (_services?.Existe(brand)??true)
			{
				ModelState.AddModelError(string.Empty, "Brand name already exists");
				return View(brandVm);
			}
			_services.Guardar(brand);
			TempData["success"] = "Record added successfully";
			return RedirectToAction("Index");
		}
	}
}
