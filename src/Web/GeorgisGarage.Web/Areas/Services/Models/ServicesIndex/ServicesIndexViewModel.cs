using System.Collections.Generic;

namespace GeorgisGarage.Web.Areas.Services.Models.ServicesIndex
{
    public class ServicesIndexViewModel
    {
        public ICollection<ServiceViewModel> LatestServices { get; set; }

        public ICollection<ServiceViewModel> TopServices { get; set; }

        public ICollection<ServiceViewModel> AllServices { get; set; }

        public ICollection<ServiceViewModel> LongestServices { get; set; }
    }
}
