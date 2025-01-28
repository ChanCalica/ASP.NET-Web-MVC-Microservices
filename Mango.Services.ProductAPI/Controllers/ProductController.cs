using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await productService.GetAllProductsAsync();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await productService.GetProductByIdAsync(id);

            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var response = await productService.CreateProductAsync(productDto);

            return Ok(response);
        }

        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            var response = await productService.UpdateProductAsync(productDto);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.DeleteProductAsync(id);

            return Ok(response);
        }
    }
}
