using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Services.Models.ServicesIndex;
using GeorgisGarage.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesIndexService servicesService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public HomeController(IServicesIndexService servicesService, IImageService imageService, IMapper mapper)
        {
            this.servicesService = servicesService;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var services = this.servicesService.GetTopServices();

            var model = new List<ServiceViewModel>();

            foreach (var service in services)
            {
                var coverPhoto = this.imageService.ReturnImageWithGiverDimensions(service.CoverPhoto.Image, 1500, 300, "limit");
                var token = mapper.Map<ServiceViewModel>(service);
                token.CoverPhoto = coverPhoto;
                model.Add(token);
            }

            return View(model);
        }
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
