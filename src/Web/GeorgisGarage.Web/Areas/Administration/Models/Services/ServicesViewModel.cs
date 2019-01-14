using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Administration.Models.Services
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
