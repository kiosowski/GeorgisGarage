using System;
using System.Collections.Generic;
using System.Text;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GeorgisGarage.Services.Contracts
{
    public interface IProductsService
    {
        Product CreateProduct(string name, string description, decimal price, IFormFile image,
            string additionalInfo);

        T Details<T>(string id);

        ICollection<Product> GetAllProducts();
        T IndexProductDetails<T>(string id);
        Product GetProductById(string id);

        Product EditProduct(string id, string name, string description, bool isHidden, string additionalInfo,
            decimal price);

    }
}
