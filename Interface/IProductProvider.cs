using ProductsMiddleware.Models;

namespace ProductsMiddleware.Interface
{
    public interface IProductProvider
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByFilter(string category = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchText);
    }
}
