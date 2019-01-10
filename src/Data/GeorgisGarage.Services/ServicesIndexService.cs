using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeorgisGarage.Data;
using GeorgisGarage.Services.Contracts;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Services
{
    public class ServicesIndexService : IServicesIndexService
    {
        private const int ImagesCountForCarousel = 5;

        private readonly ApplicationDbContext context;

        public ServicesIndexService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ICollection<Service> GetAllServices()
        {
            var roads = this.context.Services.Take(ImagesCountForCarousel).ToList();

            return roads;
        }

        public ICollection<Service> GetLatestServices()
        {
            var roads = this.context.Services.OrderByDescending(x => x.PostedOn).Take(ImagesCountForCarousel).ToList();

            return roads;
        }

        public ICollection<Service> GetLongestServices()
        {
            var roads = this.context.Services.OrderByDescending(x => x.ServiceTime).Take(ImagesCountForCarousel).ToList();

            return roads;
        }

        public ICollection<Service> GetTopServices()
        {
            var roads = this.context.Services.OrderByDescending(x => x.AverageRating)
                .Take(ImagesCountForCarousel)
                .ToList()
                .OrderByDescending(x => x.AverageRating)
                .ToList();

            return roads;
        }

        public ICollection<Service> GetCurrentUserServicesById(string id)
        {
            var roads = this.context.Services.Where(x => x.User.Id == id).ToList();
            return roads;
        }
    }
}

