using System;
using System.Collections.Generic;
using System.Text;

namespace GeorigsGarage.Data.Models
{
    public class Cart
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<CartProduct> Products { get; set; }
    }
}
