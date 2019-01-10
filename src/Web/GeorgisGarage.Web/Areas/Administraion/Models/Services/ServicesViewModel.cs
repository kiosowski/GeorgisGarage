using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Administraion.Models.Services
{
    public class ServicesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public User PostedBy { get; set; }

        public int Comments { get; set; }

        public double PosterRating { get; set; }

        public double Rating { get; set; }
    }
}
