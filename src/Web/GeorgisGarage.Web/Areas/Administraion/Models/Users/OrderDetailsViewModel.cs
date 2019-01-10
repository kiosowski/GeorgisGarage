using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Areas.Administraion.Models.Users
{
    public class OrderDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal PriceForOne { get; set; }

        public decimal Total { get; set; }
    }
}
