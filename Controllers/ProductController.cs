using Microsoft.AspNetCore.Mvc;
using ProductsMiddleware.Interface;
using ProductsMiddleware.Models;
using ProductsMiddleware.Services;

namespace ProductsMiddleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts(string category = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            try
            {
               var filteredProducts = await _productService.GetProductByFilter(category, minPrice, maxPrice);

                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Greška prilikom filtriranja proizvoda: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string searchText)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    return BadRequest("Unesite tekst pretrage.");
                }

                var products = await _productService.GetProductsAsync();

                // Filtriranje proizvoda po nazivu koji sadrži uneseni tekst
                products = products.Where(p => p.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase));

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Greška prilikom pretrage proizvoda: {ex.Message}");
            }
        }
    }

    
}
