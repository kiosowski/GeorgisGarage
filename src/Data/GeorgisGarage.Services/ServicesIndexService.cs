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
            var services = this.context.Services.Take(ImagesCountForCarousel).ToList();

            return services;
        }

        public ICollection<Service> GetLatestServices()
        {
            var services = this.context.Services.OrderByDescending(x => x.PostedOn).Take(ImagesCountForCarousel).ToList();

            return services;
        }

        public ICollection<Service> GetLongestServices()
        {
            var services = this.context.Services.OrderByDescending(x => x.ServiceTime).Take(ImagesCountForCarousel).ToList();

            return services;
        }

        public ICollection<Service> GetTopServices()
        {
            var services = this.context.Services.OrderByDescending(x => x.AverageRating)
                .Take(ImagesCountForCarousel)
                .ToList()
                .OrderByDescending(x => x.AverageRating)
                .ToList();

            return services;
        }

        public ICollection<Service> GetCurrentUserServicesById(string id)
        {
            var services = this.context.Services.Where(x => x.User.Id == id).ToList();
            return services;
        }
    }
}

