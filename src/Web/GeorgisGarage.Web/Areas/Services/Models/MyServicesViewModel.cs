using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Areas.Services.Models
{
    public class MyServicesViewModel
    {
        public string Id { get; set; }

        public string CoverPhotoUrl { get; set; }

        public DateTime PostedOn { get; set; }

        public string ServiceName { get; set; }
    }
}
