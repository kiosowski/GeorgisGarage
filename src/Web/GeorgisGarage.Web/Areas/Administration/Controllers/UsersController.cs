using System.Collections.Generic;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Administration.Models.Users;
using GeorgisGarage.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IServicesService servicesService;
        private readonly IServicesIndexService servicesIndexService;
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public UsersController(IUsersService usersService, IServicesService servicesService,
            IServicesIndexService servicesIndexService, IOrdersService ordersService, IMapper mapper)
        {
            this.usersService = usersService;
            this.servicesService = servicesService;
            this.servicesIndexService = servicesIndexService;
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var users = this.usersService.GetAllUsers();

            var model = mapper.Map<List<UsersTableViewModel>>(users);

            foreach (var user in model)
            {
                user.Role = this.usersService.GetUserRole(user.Username);
            }

            return View(model);
        }

        public IActionResult UsersRoads(string id)
        {
            var roads = this.servicesIndexService.GetCurrentUserServicesById(id);

            var model = new UsersServicesViewModelWrapper
            {
                User = this.usersService.GetUserById(id),
                UsersRoadsViewModels = mapper.Map<List<UsersServicesViewModel>>(roads)
            };

            return this.View(model);
        }

        public IActionResult UsersOrders(string id)
        {
            var user = this.usersService.GetUserById(id);
            var orders = this.ordersService.GetCurrentUserOrders(user.UserName);

            var model = new UsersOrdersWrapperViewModel
            {
                User = this.usersService.GetUserById(id),
                UsersOrders = mapper.Map<List<UsersOrdersViewModel>>(orders)
            };

            return this.View(model);
        }

        public IActionResult PromoteUser(string id)
        {

            bool isPromoted = this.usersService.PromoteUserToAdminRole(id);

            if (!isPromoted)
            {
                return NotFound();
            }

            return this.RedirectToAction("All", "Users");
        }

        public IActionResult DemoteUser(string id)
        {
            bool isDemoted = this.usersService.DemoteUserToUserRole(id);

            if (!isDemoted)
            {
                return NotFound();
            }

            return this.RedirectToAction("All", "Users");
        }
    }
}