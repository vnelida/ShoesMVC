using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Web.ViewModels.Genres;
using X.PagedList.Extensions;

namespace Shoes.Web.Controllers
{
	public class GenresController : Controller
	{
		private readonly IGenresService? _services;
		private readonly IMapper? _mapper;

		public GenresController(IGenresService? services, IMapper? mapper)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

		}

		public IActionResult Index(int? page)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			var genre = _services?.GetAll(orderBy: o => o.OrderBy(c => c.GenreName));
			var genreVm = _mapper?.Map<List<GenreListVm>>(genre).ToPagedList(pageNumber, pageSize);
			return View(genreVm);
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
	}
}
