using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shoes.Entidades;
using Shoes.Servicios.Interface;
using Shoes.Servicios.Servicios;
using Shoes.Web.Models;
using Shoes.Web.ViewModels.Shoes;
using System.ComponentModel;
using System.Diagnostics;
using X.PagedList.Extensions;

namespace Shoes.Web.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly IShoesService? _shoesService;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 8;

        public HomeController(IShoesService? shoesService, IMapper? mapper)
        {
            _shoesService = shoesService;
            _mapper = mapper;
        }
        public IActionResult Headline()
        {
            return View();
        }
        public IActionResult Index(int? page)
        {
            var currentPage = page ?? 1;
            var shoes=_shoesService!.GetAll(orderBy: o => o.OrderBy(b => b.Model), propertiesNames: "Brand,ColorN,Sport,Genre");
            var shoesVm=_mapper!.Map<List<ShoeHomeIndexVm>>(shoes);
            return View(shoesVm.ToPagedList(currentPage, pageSize));
        }
        public IActionResult Details(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }

            Shoe? shoe = _shoesService!.Get(
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
            }).ToList();


            return View(shoeVm);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
