﻿namespace GeorgisGarage.Web.Areas.Shop.Models
{
    public class AllProductsDetailsViewModel
    {
        public string Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsHidden { get; set; }
    }
}