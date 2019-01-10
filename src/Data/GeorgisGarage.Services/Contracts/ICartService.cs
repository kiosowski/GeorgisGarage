using System.Collections.Generic;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Services.Contracts
{
    public interface ICartService
    {
        IEnumerable<CartProduct> GetAllCartProducts(string username);
        void AddProductToCart(string productId, string username, int? quantity = null);
        void EditProductInCart(string productId, string username, int quantity);
        void DeleteProductFromCart(string id, string username);
        bool AnyProducts(string username);
        void DeleteAllProductsFromCart(string username);
    }
}
