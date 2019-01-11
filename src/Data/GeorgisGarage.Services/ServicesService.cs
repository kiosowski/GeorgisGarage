using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GeorgisGarage.Data;
using GeorgisGarage.Services.Contracts;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GeorgisGarage.Services
{
    public class ServicesService : IServicesService
    {
        private const int ServicesShownOnPage = 10;
        private readonly ApplicationDbContext context;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IVideoService videoService;
        private readonly IUsersService usersService;

        public ServicesService(ApplicationDbContext context, IImageService imageService, IMapper mapper,
            IVideoService videoService, UserManager<User> userManager, IUsersService usersService)
        {
            this.context = context;
            this.imageService = imageService;
            this.mapper = mapper;
            this.videoService = videoService;
            this.userManager = userManager;
            this.usersService = usersService;
        }
        

        public bool Edit(string serviceId, string serviceName, string startTime, string endTime, double serviceTime,
            string description, string video, IFormFile imageFromForm, int viewRating, int surfaceRating,
            int pleasureRating)
        {
            if (serviceName == null || startTime == null || endTime == null || serviceTime == 0.00 ||
                description == null)
                return false;

            string embedYoutubeUrl = null;

            if (video != null)
            {
                embedYoutubeUrl = videoService.ReturnEmbedYoutubeLink(video);
            }

            var service = this.context.Services.FirstOrDefault(x => x.Id == serviceId);
            if (service == null) return false;

            service.ServiceName = serviceName;
            service.Description = description;
            service.StartedTime = startTime;
            service.EndTime = endTime;
            service.ServiceTime = serviceTime;
            service.SurfaceRating = surfaceRating;
            service.ViewRating = viewRating;
            service.PleasureRating = pleasureRating;

            if (service.Video != video)
            {
                service.Video = embedYoutubeUrl;
            }

            context.SaveChanges();

            if (imageFromForm != null)
            {
                service.CoverPhoto = null;

                var image = imageService.AddPhoto(imageFromForm);

                image.Name = serviceName + "main";

                var coverPhoto = new CoverPhotoService { Image = image, ImageId = image.Id, Service = service, ServiceId = service.Id };

                context.CoverPhotoService.Add(coverPhoto);
                context.SaveChanges();

                service.CoverPhoto = coverPhoto;
                service.CoverPhotoId = coverPhoto.Id;

                context.SaveChanges();
            }

            return true;
        }

        public Service GetServiceById(string id)
        {
            var service = this.context.Services.FirstOrDefault(x => x.Id == id);
            return service;
        }

        public bool Create(string serviceName, string startTime, string endTime, double serviceTime,
            string description, string video, string userId, IFormFile imageFromForm, ICollection<IFormFile> photos,
            int viewRating, int surfaceRating, int pleasureRating)
        {
            if (serviceName == null || startTime == null || endTime == null || serviceTime == 0.00 ||
                description == null || userId == null || imageFromForm == null)
                return false;

            if (this.context.Services.Any(x => x.ServiceName == serviceName))
            {
                return false;
            }

            string embedYoutubeUrl = null;

            if (video != null)
            {
                embedYoutubeUrl = videoService.ReturnEmbedYoutubeLink(video);
            }

            var imageList = new List<Image>();

            foreach (var photo in photos)
            {
                imageList.Add(this.imageService.AddPhoto(photo));
            }

            Service service = new Service
            {
                ServiceName = serviceName,
                Description = description,
                EndTime = endTime,
                PostedOn = DateTime.UtcNow,
                StartedTime = startTime,
                ServiceTime = serviceTime,
                Video = embedYoutubeUrl,
                UserId = userId,
                Photos = imageList,
                SurfaceRating = surfaceRating,
                ViewRating = viewRating,
                PleasureRating = pleasureRating
            };

            context.Services.Add(service);
            context.SaveChanges();

            var image = imageService.AddPhoto(imageFromForm);

            image.Name = serviceName + "main";

            var coverPhoto = new CoverPhotoService { Image = image, ImageId = image.Id, Service = service, ServiceId = service.Id };

            context.CoverPhotoService.Add(coverPhoto);
            context.SaveChanges();

            service.CoverPhoto = coverPhoto;
            service.CoverPhotoId = coverPhoto.Id;

            context.SaveChanges();
            return true;
        }

        public T Details<T>(string id)
        {
            var service = this.context.Services.Find(id);

            if (service == null)
            {
                return default(T);
            }

            var model = this.mapper.Map<T>(service);

            return model;
        }

        public ICollection<Service> GetServices()
        {
            return context.Services.Include(x => x.CoverPhoto).Include(x => x.Photos).ToList();
        }

        public ICollection<Service> GetLatestServices()
        {
            return this.context.Services.Include(x => x.CoverPhoto)
                .Include(x => x.Photos)
                .Include(x => x.User)
                .OrderByDescending(x => x.PostedOn)
                .Take(ServicesShownOnPage)
                .ToList();
        }

        public ICollection<Service> GetLongestServices()
        {
            return this.context.Services.Include(x => x.CoverPhoto)
                .Include(x => x.Photos)
                .Include(x => x.User)
                .OrderByDescending(x => x.ServiceTime)
                .Take(ServicesShownOnPage)
                .ToList();
        }

        public ICollection<Service> GetTopServices()
        {
            var services = this.context.Services.OrderByDescending(x => x.AverageRating)
                .Take(ServicesShownOnPage)
                .ToList()
                .OrderByDescending(x => x.AverageRating)
                .ToList();

            return services;
        }

        public Service GetServiceByImage(Image image)
        {
            var img = this.context.Images.First(x => x.Id == image.Id);
            var road = this.context.Services.Find(img.Service.Id);
            return road;
        }

        public bool AddImagesToService(ICollection<IFormFile> images, string roadId)
        {
            var service = this.GetServiceById(roadId);

            if (service == null) return false;

            foreach (var image in images)
            {
                service.Photos.Add(this.imageService.AddPhoto(image));
            }

            this.context.SaveChanges();

            return true;
        }

        public bool DeleteService(string id)
        {
            var service = this.context.Services.FirstOrDefault(x => x.Id == id);


            if (service == null) return false;

            var coverPhotoRoad = this.context.CoverPhotoService.FirstOrDefault(x => x.ServiceId == service.Id);

            if (coverPhotoRoad != null)
            {
                this.context.CoverPhotoService.Remove(coverPhotoRoad);
            }


            this.context.Services.Remove(service);
            this.context.SaveChanges();

            return true;
        }
    }
}