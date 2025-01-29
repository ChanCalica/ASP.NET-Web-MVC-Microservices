using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Services;

public class ProductService(AppDbContext appDbContext, IMapper mapper) : IProductService
{
    private ResponseDto _response = new();

    public async Task<ResponseDto> CreateProductAsync(ProductDto productDto)
    {
        try
        {
            var product = mapper.Map<Product>(productDto);

            await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();

            var result = mapper.Map<ProductDto>(product);
            _response.Results = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> DeleteProductAsync(int productId)
    {
        try
        {
            var product = await appDbContext.Products.FirstAsync(p => p.ProductId == productId);

            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> GetAllProductsAsync()
    {
        try
        {
            var products = await appDbContext.Products.ToListAsync();
            _response.Results = mapper.Map<IEnumerable<ProductDto>>(products);

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> GetProductByIdAsync(int productId)
    {
        try
        {
            var product = await appDbContext.Products.FirstAsync(product => product.ProductId == productId);
            _response.Results = mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto)
    {
        try
        {
            var product = mapper.Map<Product>(productDto);

            appDbContext.Products.Update(product);
            await appDbContext.SaveChangesAsync();

            _response.Results = mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
