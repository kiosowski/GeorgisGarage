using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Administraion.Models.Orders;
using GeorgisGarage.Web.Areas.Administraion.Models.Users;
using GeorgisGarage.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Administraion.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var orders = this.ordersService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }

            var model = mapper.Map<List<OrderViewModel>>(orders);
            return this.View(model);
        }

        public IActionResult Processed()
        {
            var orders = this.ordersService.GetProcessedOrders();

            if (orders == null)
            {
                return NotFound();
            }

            var model = mapper.Map<List<OrderViewModel>>(orders);
            return this.View(model);
        }

        public IActionResult Sent()
        {
            var orders = this.ordersService.GetSentOrders();

            if (orders == null)
            {
                return NotFound();
            }

            var model = mapper.Map<List<OrderViewModel>>(orders);
            return this.View(model);
        }

        public IActionResult Delivered()
        {
            var orders = this.ordersService.GetDeliveredOrders();

            if (orders == null)
            {
                return NotFound();
            }

            var model = mapper.Map<List<OrderViewModel>>(orders);
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var orderProducts = this.ordersService.GetOrderDetails(id);
            var model = mapper.Map<List<OrderDetailsViewModel>>(orderProducts);

            return this.View(model);
        }

        public IActionResult Deliver(string id)
        {
            this.ordersService.DeliverOrder(id);

            return this.RedirectToAction("Index", "AdministrationIndex");
        }

        public IActionResult Send(string id)
        {
            this.ordersService.SendOrder(id);

            return this.RedirectToAction("Index", "AdministrationIndex");
        }
    }
}
