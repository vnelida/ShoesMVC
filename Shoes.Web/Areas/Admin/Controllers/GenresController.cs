using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Genres;
using Shoes.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace Shoes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenresController : Controller
    {
        private readonly IShoesService? _shoeService;
        private readonly IGenresService? _services;
        private readonly IMapper? _mapper;

        public GenresController(IGenresService? services, IShoesService? shoeService, IMapper? mapper)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _shoeService = shoeService ?? throw new ArgumentNullException(nameof(services));

        }

        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Genre>? genres;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    genres = _services?
                        .GetAll(orderBy: o => o.OrderBy(c => c.GenreName),
                            filter: c => c.GenreName.Contains(searchTerm)).ToList();
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    genres = _services?
                        .GetAll(orderBy: o => o.OrderBy(c => c.GenreName)).ToList();
                }

            }
            else
            {
                genres = _services?
                    .GetAll(orderBy: o => o.OrderBy(c => c.GenreName)).ToList();

            }
            var genresVm = _mapper?.Map<List<GenreListVm>>(genres)
                .ToPagedList(pageNumber, pageSize);

            foreach (var genre in genresVm!)
            {
                genre.ShoesQuantity = GetShoeQuantity(genre.GenreId);
            }
            return View(genresVm);
        }
        public IActionResult UpSert(int? id)
        {
            if (_services is null || _mapper is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
            }

            GenreEditVm genreVm;
            if (id is null || id == 0)
            {
                genreVm = new GenreEditVm();
            }
            else
            {
                Genre? genre = _services?.Get(filter: c => c.GenreId == id);
                if (genre is null)
                {
                    return NotFound();
                }
                genreVm = _mapper.Map<GenreEditVm>(genre);
            }


            return View(genreVm);
        }
        [HttpPost]
        public IActionResult UpSert(GenreEditVm genreVm)
        {
            if (!ModelState.IsValid)
            {
                return View(genreVm);
            }
            if (_services is null || _mapper is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
            }
            Genre? genre = _mapper.Map<Genre>(genreVm);

            try
            {
                if (_services.Exist(genre))
                {
                    ModelState.AddModelError(string.Empty, "Item already exist");
                    return View(genreVm);
                }
                _services.Save(genre);
                TempData["success"] = "Record added/edited successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(genre);
            }
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Genre? genre = _services?.Get(filter: c => c.GenreId == id);
            if (genre is null)
            {
                return NotFound();
            }

            try
            {
                if (_services is null || _mapper is null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "The dependencies are not configured correctly");
                }
                if (_services.IsRelated(genre.GenreId))
                {
                    return Json(new { success = false, message = "Related record. Delete denny" });
                }
                _services.Delete(genre);
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
            Genre? genre = _services?.Get(filter: c => c.GenreId == id);
            if (genre is null)
            {
                return NotFound();
            }
            if (_services.IsRelated(genre.GenreId))
            {
                ModelState.AddModelError(string.Empty, "Record is associated with other and cannot be deleted ");
                return View(genre);
            }
            _services.Delete(genre);
            TempData["success"] = "Record deleted successfully";
            return RedirectToAction("Index");
        }
        private int GetShoeQuantity(int genreId)
        {
            return _shoeService!.GetAll(
                    filter: p => p.GenreId == genreId)!.Count();
        }
        public IActionResult Details(int? id, int? page)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Genre? genre = _services?.Get(filter: g => g.GenreId == id);
            if (genre is null)
            {
                return NotFound();
            }

            var currentPage = page ?? 1;
            int pageSize = 10;

            var genreVm = _mapper!.Map<GenreDetailsVm>(genre);
            genreVm.ShoesQuantity = GetShoeQuantity(genreVm.GenreId);
            var shoes = _shoeService!.GetAll(
                orderBy: o => o.OrderBy(s => s.Model),
                filter: s => s.GenreId == genreVm.GenreId,
                propertiesNames: "ColorN,Brand,Genre,Sport");
            genreVm.Shoes = _mapper!.Map<List<ShoeListVm>>(shoes);

            return View(genreVm);
        }

    }
}
