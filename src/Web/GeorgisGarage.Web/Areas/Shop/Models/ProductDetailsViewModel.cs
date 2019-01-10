using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Shop.Models
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public string Description { get; set; }

        public string AdditionalInfo { get; set; }

        public ProductImage Image { get; set; }
    }
}
