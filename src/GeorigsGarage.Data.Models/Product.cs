﻿using System.Collections.Generic;

namespace GeorigsGarage.Data.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string ImageId { get; set; }
        public virtual ProductImage Image { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public string AdditionalInfo { get; set; }

        public virtual ICollection<CartProduct> CartProducts { get; set; }

        public bool IsHidden { get; set; }

    }
}