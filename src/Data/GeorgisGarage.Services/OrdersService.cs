﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeorgisGarage.Data;
using GeorgisGarage.Services.Contracts;
using GeorigsGarage.Data.Models;
using GeorigsGarage.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GeorgisGarage.Services
{
    public class OrdersService : IOrdersService
    {
        private const int EstimatedDeliveryDateAddDays = 5;
        private readonly ICartService cartService;
        private readonly IUsersService usersService;
        private readonly ApplicationDbContext context;

        public OrdersService(ApplicationDbContext context, IUsersService usersService, ICartService cartService)
        {
            this.context = context;
            this.usersService = usersService;
            this.cartService = cartService;
        }

        public Order CreateOrder(string username)
        {
            var user = this.usersService.GetUserByUsername(username);

            var processingOrder = this.GetProcessingOrder(username);
            if (processingOrder != null)
            {
                return processingOrder;
            }

            var order = new Order { OrderStatus = OrderStatus.Processing, User = user };

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            return order;
        }

        public Order GetProcessingOrder(string username)
        {
            var user = this.usersService.GetUserByUsername(username);

            var order = this.context.Orders.Include(x => x.OrderProducts)
                .FirstOrDefault(x => x.User.UserName == username && x.OrderStatus == OrderStatus.Processing);

            return order;
        }

        public void MakeOrder(Order order, string fullname, string phoneNumber, string address, string city,
            string additionalInformation)
        {
            order.Address = address;
            order.City = city;
            order.AdditionalInformation = additionalInformation;
            order.RecipientName = fullname;
            order.RecipientPhoneNumber = phoneNumber;

            this.context.Update(order);
            this.context.SaveChanges();
        }

        public void CompleteOrder(string username)
        {
            var order = this.GetProcessingOrder(username);
            if (order == null) return;

            var cartProducts = this.cartService.GetAllCartProducts(username).ToList();
            if (cartProducts.Count == 0) return;

            List<OrderProduct> orderProducts = new List<OrderProduct>();

            foreach (var product in cartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    Product = product.Product,
                    Quantity = product.Quantity
                };

                orderProducts.Add(orderProduct);
            }

            this.cartService.DeleteAllProductsFromCart(username);

            order.OrderDate = DateTime.UtcNow;
            order.OrderStatus = OrderStatus.Processed;
            order.OrderProducts = orderProducts;
            order.TotalPrice = order.OrderProducts.Sum(x => x.Quantity * x.Product.Price);

            this.context.SaveChanges();
        }

        public Order GetOrderById(string id)
        {
            var order = this.context.Orders.FirstOrDefault(x => x.Id == id);

            return order;
        }

        public void SendOrder(string id)
        {
            var order = this.GetOrderById(id);
            var currentOrderStatus = order.OrderStatus;

            if (currentOrderStatus != OrderStatus.Processed)
            {
                return;
            }

            var newOrderStatus = OrderStatus.Sent;
            order.OrderStatus = newOrderStatus;
            order.EstimatedDeliveryDate = DateTime.Today.AddDays(EstimatedDeliveryDateAddDays);
            this.context.Update(order);
            this.context.SaveChanges();
        }

        public void DeliverOrder(string id)
        {
            var order = this.GetOrderById(id);
            var currentOrderStatus = order.OrderStatus;
            if (currentOrderStatus != OrderStatus.Sent)
            {
                return;
            }

            var newOrderStatus = OrderStatus.Delivered;
            order.OrderStatus = newOrderStatus;
            this.context.Update(order);
            this.context.SaveChanges();
        }

        public List<Order> GetCurrentUserOrders(string username)
        {
            var orders = this.context.Orders.Where(x => x.User.UserName == username);

            return orders.ToList();
        }

        public List<OrderProduct> GetOrderDetails(string id)
        {
            var orderProduct = this.context.OrderProducts.Where(x => x.OrderId == id).ToList();

            if (orderProduct.Count == 0)
            {
                return null;
            }

            return orderProduct;
        }

        public List<Order> GetAllOrders()
        {
            return this.context.Orders.ToList();
        }

        public List<Order> GetProcessedOrders()
        {
            return this.context.Orders.Where(x => x.OrderStatus == OrderStatus.Processed).ToList();
        }

        public List<Order> GetSentOrders()
        {
            return this.context.Orders.Where(x => x.OrderStatus == OrderStatus.Sent).ToList();
        }

        public List<Order> GetDeliveredOrders()
        {
            return this.context.Orders.Where(x => x.OrderStatus == OrderStatus.Delivered).ToList();
        }
    }
}
