using Microsoft.AspNetCore.Mvc;
using ProductsMiddleware.Interface;
using ProductsMiddleware.Models;

namespace ProductsMiddleware.Services
{
    public class ProductService
    {
        private readonly IProductProvider _productRepository;

        public ProductService(IProductProvider productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();

            if (products == null)
            {
                throw new Exception("Nema dostupnih proizvoda.");
            }

            
            foreach (var product in products)
            {
                if (product.Description.Length > 100)
                {
                    product.Description = product.Description.Substring(0, 100);
                }
            }

            return products;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByFilter(string category = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var products = await _productRepository.GetProductsAsync();

            if (products == null)
            {
                throw new Exception("Failed to retrieve products.");
            }

            
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.ToLower() == category.ToLower());
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            return products.ToList();
        }

        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string searchText)
        {
            
            if (string.IsNullOrEmpty(searchText))
            {
                throw new Exception("No searchText.");
            }

            var products = await _productRepository.GetProductsAsync();

            // Filtriranje proizvoda po nazivu koji sadrži uneseni tekst
            products = products.Where(p => p.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase));

            return products.ToList();
        }
            
        

    }
}
