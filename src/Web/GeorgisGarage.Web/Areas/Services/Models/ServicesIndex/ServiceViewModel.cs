using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Areas.Services.Models.ServicesIndex
{
    public class ServiceViewModel
    {
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public string CoverPhoto { get; set; }

        public DateTime PostedOn { get; set; }

        public string PostedBy { get; set; }
    }
}