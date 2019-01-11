using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Services.Models.ServicesIndex;
using GeorgisGarage.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Services.Controllers
{
    [Area(Constants.RoadsArea)]
    public class CategoriesController : Controller
    {
        private readonly IImageService imageService;
        private readonly IServicesService serviceService;
        private readonly IMapper mapper;

        public CategoriesController(IImageService imageService, IServicesService serviceService, IMapper mapper)
        {
            this.imageService = imageService;
            this.serviceService = serviceService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var services = this.serviceService.GetServices();
            List<ServiceViewModel> serviceModel = MapServicesViewModel(services);
            return View(serviceModel);
        }

        public IActionResult LatestServices()
        {
            var services = this.serviceService.GetLatestServices();
            List<ServiceViewModel> serviceModel = MapServicesViewModel(services);

            return View(serviceModel);
        }

        public IActionResult LongestServices()
        {
            var services = this.serviceService.GetLongestServices();
            List<ServiceViewModel> serviceModel = MapServicesViewModel(services);

            return View(serviceModel);
        }

        public IActionResult TopServices()
        {
            var services = this.serviceService.GetTopServices();
            List<ServiceViewModel> serviceModel = MapServicesViewModel(services);

            return View(serviceModel);
        }

        private List<ServiceViewModel> MapServicesViewModel(ICollection<GeorigsGarage.Data.Models.Service> services)
        {
            var serviceModel = new List<ServiceViewModel>();

            foreach (var service in services)
            {
                var coverPhoto = this.imageService.ReturnImage(service.CoverPhoto.Image);
                var token = mapper.Map<ServiceViewModel>(service);
                token.CoverPhoto = coverPhoto;
                serviceModel.Add(token);
            }

            return serviceModel;
        }
    }
}