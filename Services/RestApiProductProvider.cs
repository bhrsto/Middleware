using System.Text.Json;
using Newtonsoft.Json.Linq;
using ProductsMiddleware.Interface;
using ProductsMiddleware.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsMiddleware.Services
{
    public class RestApiProductProvider : IProductProvider
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://dummyjson.com/products";

        public RestApiProductProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync(BaseUrl);

            var productList = JsonSerializer.Deserialize<ProductList>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (productList == null || productList.Products == null)
            {
                throw new Exception("Neuspjela deserializacija JSON odgovora");
            }

            return productList.Products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/{id}");
            return JsonSerializer.Deserialize<Product>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        public Task<Product> GetProductByFilter(string category = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchProductsAsync(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
