using System;
using System.Collections.Generic;
using System.Text;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GeorgisGarage.Services.Contracts
{
    public interface IImageService
    {
        Image AddPhoto(IFormFile formFile);

        string ReturnImageWithGiverDimensions(Image image, int width, int height, string crop);
        string ReturnImage(Image image);
        void RemoveImage(string imageId);
        Image FindImageById(string id);
        ProductImage AddImageToProduct(IFormFile photo);
        string ReturnProductImage(ProductImage image);
    }
}
