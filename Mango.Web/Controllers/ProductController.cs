using Mango.Web.Models.Dto.ProductDto;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class ProductController(IProductService productService) : Controller
{
    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> productList = new List<ProductDto>();

        var response = await productService.GetALlProductsAsync();

        if (response != null && response.IsSuccess)
        {
            productList = JsonConvert.DeserializeObject<List<ProductDto>>(response.Results.ToString());
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(productList);
    }

    public IActionResult ProductCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductDto obj)
    {
        if (ModelState.IsValid)
        {
            var response = await productService.CreateProductAsync(obj);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }

        return View(obj);
    }

    public async Task<IActionResult> ProductDelete(int productId)
    {
        var response = await productService.GetProductByIdAsync(productId);

        if (response != null && response.IsSuccess)
        {
            var productDto = JsonConvert.DeserializeObject<ProductDto>(response.Results.ToString());

            return View(productDto);
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductDto obj)
    {
        var response = await productService.DeleteProductAsync(obj.ProductId);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(obj);
    }

    public async Task<IActionResult> ProductEdit(int productId)
    {
        var response = await productService.GetProductByIdAsync(productId);

        if (response != null && response.IsSuccess)
        {
            var productDto = JsonConvert.DeserializeObject<ProductDto>(response.Results.ToString());

            return View(productDto);
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductEdit(ProductDto obj)
    {
        var response = await productService.UpdateProductAsync(obj);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Product updated successfully";
            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(obj);
    }
}
