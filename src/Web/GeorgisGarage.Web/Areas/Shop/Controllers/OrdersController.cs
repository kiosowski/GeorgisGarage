﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Shop.Models;
using GeorgisGarage.Web.Areas.Shop.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Shop.Controllers
{
    [Authorize]
    [Area("Shop")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IUsersService cartService, ICartService cartService1, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.usersService = cartService;
            this.cartService = cartService1;
            this.mapper = mapper;
        }


        public IActionResult Create()
        {
            if (!this.cartService.AnyProducts(this.User.Identity.Name))
            {
                return this.RedirectToAction("All", "Products", new { area = "Shop" });
            }

            var order = this.ordersService.CreateOrder(this.User.Identity.Name);

            var user = this.usersService.GetUserByUsername(this.User.Identity.Name);
            var fullName = $"{user.FirstName} {user.LastName}";

            var createOrderViewModel = new CreateOrderViewModel
            {
                FullName = fullName,
                PhoneNumber = user.PhoneNumber
            };


            return View(createOrderViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            if (!this.cartService.AnyProducts(this.User.Identity.Name))
            {
                return this.RedirectToAction("All", "Products", new { area = "Shop" });

            }

            if (!ModelState.IsValid) return this.View(model);

            var order = this.ordersService.GetProcessingOrder(this.User.Identity.Name);

            if (order == null)
            {
                return this.RedirectToAction("MyCart", "Cart");
            }

            this.ordersService.MakeOrder(order, model.FullName, model.PhoneNumber, model.Address, model.City,
                model.AdditionalInformation);

            return this.RedirectToAction(nameof(ConfirmOrder));
        }

        public IActionResult ConfirmOrder()
        {
            if (!this.cartService.AnyProducts(this.User.Identity.Name))
            {
                return this.Redirect("Error");
            }

            var order = this.ordersService.GetProcessingOrder(this.User.Identity.Name);
            var model = mapper.Map<ConfirmOrderViewModel>(order);

            return this.View(model);
        }

        public IActionResult CompleteOrder(string id)
        {
            if (!this.cartService.AnyProducts(this.User.Identity.Name))
            {
                return this.Redirect("Error");
            }

            this.ordersService.CompleteOrder(this.User.Identity.Name);

            return this.RedirectToAction("MyCart", "Cart");
        }

        public IActionResult MyOrders()
        {
            var orders = this.ordersService.GetCurrentUserOrders(this.User.Identity.Name);

            var model = new List<CurrentUserOrders>();

            foreach (var order in orders)
            {
                model.Add(new CurrentUserOrders
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    OrderStatus = order.OrderStatus.ToString(),
                    EstimatedDeliveryDate = order.EstimatedDeliveryDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) == null ? ("Not sent yet") : order.EstimatedDeliveryDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    TotalPrice = order.TotalPrice
                });
            }

            return this.View(model);
        }
    }
}