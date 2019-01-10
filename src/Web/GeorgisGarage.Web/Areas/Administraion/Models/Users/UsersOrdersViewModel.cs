using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Areas.Administraion.Models.Users
{
    public class UsersOrdersViewModel
    {
        public string Id { get; set; }

        public string OrderStatus { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string EstimatedDeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}