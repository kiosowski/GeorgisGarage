using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Areas.Administraion.Models.Users
{
    public class UsersServicesViewModel
    {
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public string AverageRating { get; set; }

        public string PostedOn { get; set; }

        public string StartedTime { get; set; }

        public string EndTime { get; set; }

        public double ServiceTime { get; set; }
    }
}

