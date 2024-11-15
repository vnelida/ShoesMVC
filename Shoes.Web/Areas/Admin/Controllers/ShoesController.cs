﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Shoes;
using X.PagedList;
using X.PagedList.Extensions;

namespace Shoes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShoesController : Controller
    {
        private readonly ISizeService _sizeSerivce;
        private readonly IShoeSizeService _shoeSizeService;
        private readonly IShoesService? _service;
        private readonly IBrandsService? _brandsService;
        private readonly IColorsService? _colorService;
        private readonly ISportsService? _sportService;
        private readonly IGenresService? _genreService;
        private readonly IWebHostEnvironment? _webHostEnvironment;
        private readonly IMapper? _mapper;

        public ShoesController(IShoesService? service, IMapper? mapper, IBrandsService brandsService, IColorsService colorService, ISportsService sportService, IGenresService genreService, IWebHostEnvironment? webHostEnvironment, IShoeSizeService shoeSizeService, ISizeService sizeSerivce)
        {
            _service = service ?? throw new ArgumentNullException(nameof(_service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_service));
            _brandsService = brandsService;
            _colorService = colorService;
            _sportService = sportService;
            _genreService = genreService;
            _webHostEnvironment = webHostEnvironment;
            _shoeSizeService = shoeSizeService;
            _sizeSerivce = sizeSerivce;
        }

        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10, int? filterId = null, string? orderBy = "Model")
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            ViewBag.currentOrderBy = orderBy;
            IEnumerable<Shoe>? shoes;
            if (filterId == 0 || filterId is null || viewAll)
            {

                if (string.IsNullOrEmpty(searchTerm) || viewAll)
                {
                    shoes = _service?.GetAll(orderBy: o => o.OrderBy(b => b.Model), propertiesNames: "Brand,ColorN,Sport,Genre");
                }
                else
                {
                    shoes = _service?.GetAll(orderBy: o => o.OrderBy(b => b.Model), propertiesNames: "Brand,ColorN,Sport,Genre", filter: m => m.Model.Contains(searchTerm) || m.Brand.BrandName.Contains(searchTerm) || m.Brand.BrandName.Contains(searchTerm) || m.ColorN.ColorName.Contains(searchTerm) || m.Genre.GenreName.Contains(searchTerm) || m.Sport.SportName.Contains(searchTerm) || m.Description.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }

            }
            else
            {
                shoes = _service?.GetAll(orderBy: o => o.OrderBy(b => b.Model), filter: m => m.BrandId == filterId, propertiesNames: "Brand,ColorN,Sport,Genre");
                ViewBag.currentFilterBrandId = filterId;
            }

            var shoesVm = _mapper?.Map<List<ShoeListVm>>(shoes);

            if (orderBy == "Sports")
            {
                shoesVm = shoesVm.OrderBy(s => s.Sport).ToList();
            }
            if (orderBy == "Price")
            {
                shoesVm = shoesVm.OrderBy(s => s.Price).ToList();
            }

            var shoeFilterVm = new ShoeFilterVm
            {
                Shoes = shoesVm!.ToPagedList(pageNumber, pageSize),
                Brands = GetBrands()

            };

            if (shoesVm != null && shoesVm.Any())
            {
                shoeFilterVm.Shoes = new PagedList<ShoeListVm>(shoesVm, pageNumber, pageSize);
            }
            else
            {
                shoeFilterVm.Shoes = new PagedList<ShoeListVm>(new List<ShoeListVm>(), pageNumber, pageSize);
            }
            return View(shoeFilterVm);
        }

        public List<SelectListItem>? GetBrands()
        {
            return _brandsService!.GetAll(orderBy: q => q.OrderBy(c => c.BrandName))
                    .Select(c => new SelectListItem
                    {
                        Text = c.BrandName,
                        Value = c.BrandId.ToString()
                    }).ToList();
        }
        public List<SelectListItem>? GetColors()
        {
            return _colorService!.GetAll(orderBy: q => q.OrderBy(c => c.ColorId))
                    .Select(c => new SelectListItem
                    {
                        Text = c.ColorName,
                        Value = c.ColorId.ToString()
                    }).ToList();

        }
        public List<SelectListItem>? GetGenres()
        {
            return _genreService!.GetAll(orderBy: q => q.OrderBy(c => c.GenreName))
                    .Select(c => new SelectListItem
                    {
                        Text = c.GenreName,
                        Value = c.GenreId.ToString()
                    }).ToList();

        }
        public List<SelectListItem>? GetSports()
        {
            return _sportService!.GetAll(orderBy: q => q.OrderBy(c => c.SportName))
                    .Select(c => new SelectListItem
                    {
                        Text = c.SportName,
                        Value = c.SportId.ToString()
                    }).ToList();

        }


        public IActionResult Upsert(int? id, string? returnUrl = null)
        {
            ShoeEditVm shoeVm;

            if (id == null || id == 0)
            {
                shoeVm = new ShoeEditVm();
                shoeVm.Brands = GetBrands();
                shoeVm.Colors = GetColors();
                shoeVm.Sports = GetSports();
                shoeVm.Genres = GetGenres();
                return View(shoeVm);


            }
            else
            {
                try
                {
                    string? wwwWebRoot = _webHostEnvironment!.WebRootPath;

                    Shoe? shoe = _service!.Get(filter: c => c.ShoeId == id);
                    if (shoe == null)
                    {
                        return NotFound();
                    }
                    if (shoe.ImageUrl != null)
                    {
                        var filePath = Path.Combine(wwwWebRoot, shoe.ImageUrl.TrimStart('/'));
                        ViewData["ImageExist"] = System.IO.File.Exists(filePath);
                    }
                    else
                    {
                        ViewData["ImageExist"] = false;
                    }

                    shoeVm = _mapper!.Map<ShoeEditVm>(shoe);
                    shoeVm.Brands = GetBrands();
                    shoeVm.Colors = GetColors();
                    shoeVm.Sports = GetSports();
                    shoeVm.Genres = GetGenres();

                    shoeVm.ReturnUrl = returnUrl;
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
            string? returnUrl = shoeVm.ReturnUrl;
            if (!ModelState.IsValid)
            {
                shoeVm.Brands = GetBrands();
                shoeVm.Colors = GetColors();
                shoeVm.Sports = GetSports();
                shoeVm.Genres = GetGenres();

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

                    shoeVm.Brands = GetBrands();
                    shoeVm.Colors = GetColors();
                    shoeVm.Sports = GetSports();
                    shoeVm.Genres = GetGenres();

                    return View(shoeVm);
                }
                string? wwwWebRoot = _webHostEnvironment!.WebRootPath;

                if (!shoeVm.RemoveImage)
                {
                    if (shoeVm.ImageFile != null)
                    {
                        var permittedExtensions = new string[] { ".png", ".jpg", ".jpeg" };
                        var fileExtension = Path.GetExtension(shoeVm.ImageFile.FileName);
                        if (!permittedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError(string.Empty, "File not allowed");
                            return View(shoeVm);

                        }
                        if (shoe.ImageUrl != null)
                        {
                            string oldFilePath = Path.Combine(wwwWebRoot, shoe.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(shoeVm.ImageFile.FileName)}";
                        string pathName = Path.Combine(wwwWebRoot, "images", "shoeImages", fileName);
                        using (var fileStream = new FileStream(pathName, FileMode.Create))
                        {
                            shoeVm.ImageFile.CopyTo(fileStream);
                        }
                        shoe.ImageUrl = $"/images/shoeImages/{fileName}";
                    }
                }
                else
                {
                    string oldFilePath = Path.Combine(wwwWebRoot, shoe.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    shoe.ImageUrl = null;
                }

                _service.Save(shoe);
                TempData["success"] = "Record successfully added/edited";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");

                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");

                shoeVm.Brands = GetBrands();
                shoeVm.Colors = GetColors();
                shoeVm.Sports = GetSports();
                shoeVm.Genres = GetGenres();


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
            Shoe? shoe = _service?.Get(filter: c => c.ShoeId == id, propertiesNames: "Brand,ColorN,Sport,Genre");
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

        public IActionResult Details(int? id, string? returnUrl)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }

            Shoe? shoe = _service!.Get(
                filter: s => s.ShoeId == id,
                propertiesNames: "Brand,ColorN,Sport,Genre,ShoesSizes.SizeN"
            );

            if (shoe is null)
            {
                return NotFound();
            }

            ShoeHomeDetailsVm shoeVm = _mapper!.Map<ShoeHomeDetailsVm>(shoe);

            shoeVm.ShoeSize = shoe.ShoesSizes.Select(ss => new ShoeSize
            {
                ShoeSizeId = ss.ShoeSizeId,
                ShoeId = ss.ShoeId,
                ShoeN = ss.ShoeN,
                SizeId = ss.SizeId,
                SizeN = ss.SizeN,
                QuantityInStock = ss.QuantityInStock
            }).OrderBy(ss => ss.SizeN.SizeNumber).ToList();
            ViewBag.ReturnUrl = returnUrl;

            return View(shoeVm);
        }
        public IActionResult UpsertShoeSize(int shoeId, string? returnUrl)
        {
            var shoe = _service!.Get(s => s.ShoeId == shoeId);

            if (shoe == null)
            {
                return NotFound();
            }

            var shoeSizeVm = new ShoeSizeVm
            {
                ShoeId = shoeId,
                Sizes = _sizeSerivce!.GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.SizeNumber.ToString(),
                        Value = c.SizeId.ToString()
                    }).ToList()
            };


            var shoeSize = _shoeSizeService.Get(ss => ss.ShoeId == shoeId);
            if (shoeSize != null)
            {
                shoeSizeVm = _mapper!.Map<ShoeSizeVm>(shoeSize);
            }
            shoeSizeVm.Sizes = _sizeSerivce!.GetAll()
                  .Select(c => new SelectListItem
                  {
                      Text = c.SizeNumber.ToString(),
                      Value = c.SizeId.ToString()
                  }).ToList();

            ViewBag.ReturnUrl = returnUrl;
            return View(shoeSizeVm);

        }



        [HttpPost]
        public IActionResult UpsertShoeSize(int shoeId, string? returnUrl, List<int> sizeIds, int quantityInStock)
        {
            if (sizeIds == null || !sizeIds.Any())
            {
                return BadRequest(new { success = false, message = "You must select at least one size." });
            }

            foreach (var sizeId in sizeIds)
            {
                var existingShoeSize = _shoeSizeService.Get(ss => ss.ShoeId == shoeId && ss.SizeId == sizeId);
                if (existingShoeSize != null)
                {
                    return BadRequest(new { success = false, message = $"The selected size(s) are already assigned to the shoe." });
                }
                var newShoeSize = new ShoeSize
                {
                    ShoeId = shoeId,
                    SizeId = sizeId,
                    QuantityInStock = quantityInStock
                };

                _shoeSizeService.Save(newShoeSize);
            }
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Json(new { success = true, message = "The size(s) have been added successfully." });
            }

        }

        [HttpPost]
        public IActionResult UpdateStock(int shoeSizeId, int newStock)
        {
            if (shoeSizeId == 0 || newStock < 0)
            {
                return BadRequest("Error.");
            }


            var shoeSize = _shoeSizeService.Get(ss => ss.ShoeSizeId == shoeSizeId);

            if (shoeSize != null)
            {
                shoeSize.QuantityInStock = newStock;
                _shoeSizeService.Save(shoeSize);
                return Json(new { success = true, message = "Stock updated successfully." });
            }
            return NotFound("ShoeSize not found.");
        }


        //public IActionResult UpsertShoeSize(ShoeSizeVm shoeSizeVm)
        //{
        //    if (shoeSizeVm.ShoeSizeId == 0)
        //    {
        //        var newShoeSize = new ShoeSize
        //        {
        //            ShoeId = shoeSizeVm.ShoeId,
        //            SizeId = shoeSizeVm.SizeId,
        //            QuantityInStock = shoeSizeVm.QuantityInStock
        //        };
        //        _shoeSizeService.Save(newShoeSize);
        //    }
        //    else
        //    {
        //        var existingShoeSize = _shoeSizeService.Get(ss => ss.ShoeSizeId == shoeSizeVm.ShoeSizeId);
        //        if (existingShoeSize == null)
        //        {
        //            return NotFound();
        //        }

        //        existingShoeSize.SizeId = shoeSizeVm.SizeId;
        //        existingShoeSize.QuantityInStock = shoeSizeVm.QuantityInStock;
        //        _shoeSizeService.Save(existingShoeSize);
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
