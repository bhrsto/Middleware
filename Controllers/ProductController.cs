using Microsoft.AspNetCore.Mvc;
using ProductsMiddleware.Interface;
using ProductsMiddleware.Models;
using ProductsMiddleware.Services;

namespace ProductsMiddleware.Controllers
{

    /// <summary>
    /// Controller for procuct management
    /// </summary>
    /// 
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// This endpoint gets all the proudcts
        /// </summary>
        /// <returns>A list of products</returns>
        /// <response code="200">It returns a list of products</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }


        /// <summary>
        /// Gets a product by its id
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Details of a product</returns>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">Product not found</response>
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

        /// <summary>
        /// Filter products by category and price
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="minPrice">Min price</param>
        /// <param name="maxPrice">Max price</param>
        /// <returns>List of filtered products</returns>
        /// <response code="200">Returns the filtered list of products</response>

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


        /// <summary>
        /// Searches products by name
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <returns>List of products matching the search</returns>
        /// <response code="200">Returns a list of products that match the search</response>
        /// <response code="400">If search text is not entered</response>

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
