using System.Collections.Generic;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Administration.Models.Services;
using GeorgisGarage.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
    public class ServicesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IServicesIndexService servicesIndexService;

        public ServicesController(IServicesIndexService servicesIndexService, IMapper mapper)
        {
            this.servicesIndexService = servicesIndexService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var roads = this.servicesIndexService.GetAllServices();

            var model = mapper.Map<List<ServicesViewModel>>(roads);

            return View(model);
        }
    }
}