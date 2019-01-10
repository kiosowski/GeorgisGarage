using System;
using System.Collections.Generic;
using System.Text;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GeorgisGarage.Services.Contracts
{
   public interface IServicesService
    {
        bool Create(string serviceName, string startedTime, string endTime, double ServiceTime, string description, string video, string userId, IFormFile photo, ICollection<IFormFile> photos, int viewRating, int surfaceRating, int pleasureRating);

        T Details<T>(string id);

        Service GetServiceById(string id);

        ICollection<Service> GetServices();

        ICollection<Service> GetLatestServices();

        ICollection<Service> GetLongestServices();

        ICollection<Service> GetTopServices();

        Service GetServiceByImage(Image image);

        bool Edit(string serviceId, string serviceName, string startedTime, string endTime, double ServiceTime,
            string description, string video, IFormFile imageFromForm,
            int viewRating, int surfaceRating, int pleasureRating);

        bool AddImagesToService(ICollection<IFormFile> images, string serviceId);

        bool DeleteService(string id);
    }
}
