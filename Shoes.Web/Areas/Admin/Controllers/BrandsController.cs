using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Brands;
using Shoes.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace Shoes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly IBrandsService? _services;
        private readonly IShoesService _shoesService;
        private readonly IMapper? _mapper;
        private readonly IWebHostEnvironment? _webHostEnvironment;
        public BrandsController(IBrandsService? services, IShoesService shoesService, IMapper mapper, IWebHostEnvironment? webHostEnvironment)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _shoesService = shoesService ?? throw new ArgumentNullException(nameof(services));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(mapper));
        }
        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Brand>? brands;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    brands = _services?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName),
                        filter: b => b.BrandName.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    brands = _services?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName));

                }

            }
            else
            {
                brands = _services?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName));

            }

            var brandVm = _mapper?.Map<List<BrandListVm>>(brands).ToPagedList(pageNumber, pageSize);
            foreach (var brand in brandVm!)
            {
                brand.ShoesQuantity = GetShoeQuantity(brand.BrandId);
            }
            return View(brandVm);
        }
        public IActionResult Details(int? id, int? page)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Brand? brand = _services?.Get(filter: g => g.BrandId == id);
            if (brand is null)
            {
                return NotFound();
            }

            var currentPage = page ?? 1;
            int pageSize = 10;

            BrandDetailsVm brandVm = _mapper!.Map<BrandDetailsVm>(brand);
            brandVm.ShoesQuantity = GetShoeQuantity(brandVm.BrandId);
            var shoes = _shoesService!.GetAll(
                orderBy: o => o.OrderBy(s => s.Model),
                filter: s => s.BrandId == brandVm.BrandId,
                propertiesNames: "ColorN,Brand,Genre,Sport");
            brandVm.Shoes = _mapper!.Map<List<ShoeListVm>>(shoes);

            return View(brandVm);
        }
        private int GetShoeQuantity(int id)
        {
            return _shoesService!.GetAll(
                        filter: s => s.BrandId == id)!.Count();
        }
        public List<SelectListItem>? GetBrands()
        {
            return _services!.GetAll(orderBy: q => q.OrderBy(c => c.BrandName))
                    .Select(c => new SelectListItem
                    {
                        Text = c.BrandName,
                        Value = c.BrandId.ToString()
                    }).ToList();
        }
        public IActionResult Upsert(int? id)
        {
            BrandEditVm brandVm;
            if (id is null || id == 0)
            {
                brandVm = new BrandEditVm();
            }
            else
            {
                try
                {
                    string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
                    Brand? brand = _services?.Get(filter: b => b.BrandId == id);
                    if (brand is null)
                    {
                        return NotFound();
                    }
                    if (brand.ImageUrl != null)
                    {
                        var filePath = Path.Combine(wwwWebRoot, brand.ImageUrl.TrimStart('/'));
                        ViewData["ImageExist"] = System.IO.File.Exists(filePath);
                    }
                    else
                    {
                        ViewData["ImageExist"] = false;
                    }
                    brandVm = _mapper!.Map<BrandEditVm>(brand);
                    return View(brandVm);

                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
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
                string? wwwWebRoot = _webHostEnvironment!.WebRootPath;

                if (!brandVm.RemoveImage)
                {
                    if (brandVm.ImageFile != null)
                    {
                        var permittedExtensions = new string[] { ".png", ".jpg", ".jpeg" };
                        var fileExtension = Path.GetExtension(brandVm.ImageFile.FileName);
                        if (!permittedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError(string.Empty, "File not allowed");
                            return View(brandVm);

                        }
                        if (brand.ImageUrl != null)
                        {
                            string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(brandVm.ImageFile.FileName)}";
                        string pathName = Path.Combine(wwwWebRoot, "images", "brandImages", fileName);
                        using (var fileStream = new FileStream(pathName, FileMode.Create))
                        {
                            brandVm.ImageFile.CopyTo(fileStream);
                        }
                        brand.ImageUrl = $"/images/brandImages/{fileName}";
                    }
                }
                else
                {
                    string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    brand.ImageUrl = null;
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
            string? wwwWebRoot = _webHostEnvironment!.WebRootPath;

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
                string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
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
