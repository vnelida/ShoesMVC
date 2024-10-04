using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shoes.Servicios.Interface;
using Shoes.Servicios.Servicios;
using Shoes.Web.Models;
using Shoes.Web.ViewModels.Shoes;
using System.Diagnostics;

namespace Shoes.Web.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly IShoesService? _shoesService;
        private readonly IMapper? _mapper;

        public HomeController(IShoesService? shoesService, IMapper? mapper)
        {
            _shoesService = shoesService;
            _mapper = mapper;
        }
        public IActionResult Headline()
        {
            return View();
        }
        public IActionResult Index()
        {
            var shoes=_shoesService!.GetAll(orderBy: o => o.OrderBy(b => b.Model), propertiesNames: "Brand,ColorN,Sport,Genre");
            var shoesVm=_mapper!.Map<List<ShoeHomeIndexVm>>(shoes);
            return View(shoesVm);
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
