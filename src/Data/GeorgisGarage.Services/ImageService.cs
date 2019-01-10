﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GeorgisGarage.Data;
using GeorgisGarage.Services.Contracts;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GeorgisGarage.Services
{
    public class ImageService : IImageService
    {
        private readonly IUsersService _usersService;
        private readonly Cloudinary _cloudinary;
        private readonly ApplicationDbContext context;

        public ImageService(ApplicationDbContext context, IUsersService usersService, Cloudinary cloudinary)
        {
            this.context = context;
            _usersService = usersService;
            _cloudinary = cloudinary;
        }

        public Image AddPhoto(IFormFile photo)
        {
            var file = photo;

            var uploadResult = new ImageUploadResult();

            string imageName = "";

            var uniqueFileName = Convert.ToString(Guid.NewGuid());
            var fileName = uniqueFileName + Path.GetExtension(photo.Name);

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams() { File = new FileDescription(uniqueFileName, stream) };
                    imageName = file.Name;
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();
            var publicId = uploadResult.PublicId;

            var image = new Image { ImageUrl = url, DateAdded = DateTime.UtcNow, PublicId = publicId, };

            return image;
        }

        public ProductImage AddImageToProduct(IFormFile photo)
        {
            var file = photo;

            var uploadResult = new ImageUploadResult();

            string imageName;

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams() { File = new FileDescription(file.Name, stream) };
                    imageName = file.Name;
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();
            var publicId = uploadResult.PublicId;

            var image = new ProductImage() { ImgUrl = url, PublicId = publicId, };

            return image;
        }

        public Image FindImageById(string id)
        {
            var image = this.context.Images.FirstOrDefault(x => x.Id == id);

            return image;
        }

        public void RemoveImage(string imageId)
        {
            var image = this.FindImageById(imageId);

            this.context.Images.Remove(image);
            this.context.SaveChanges();
        }


        public string ReturnImage(Image image)
        {
            string url = _cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(2000).Height(300).Crop("fill").Gravity("center"))
                .BuildUrl(image.PublicId);

            return url;
        }

        public string ReturnImageWithGiverDimensions(Image image, int width, int height, string crop)
        {
            string url = _cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(width).Height(height).Crop(crop))
                .BuildUrl(image.PublicId);

            return url;
        }

        public string ReturnProductImage(ProductImage image)
        {
            string url = _cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(300).Height(350).Crop("fill"))
                .BuildUrl(image.PublicId);

            return url;
        }
    }
}