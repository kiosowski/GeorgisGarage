using System.Collections.Generic;

namespace GeorgisGarage.Web.Areas.Shop.Models
{
    public class AllProductsViewModel
    {
        public ICollection<AllProductsDetailsViewModel> AllProductsDetails { get; set; }
    }
}
