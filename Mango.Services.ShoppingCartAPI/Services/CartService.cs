using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Services;

public class CartService(AppDbContext appDbContext, IMapper mapper, IProductService productService) : ICartService
{
    private ResponseDto _response = new();

    public async Task<ResponseDto> GetCartAsync(string userId)
    {
        try
        {
            var cartDto = new CartDto()
            {
                CartHeader = mapper.Map<CartHeaderDto>(appDbContext.CartHeaders.First(c => c.UserId == userId))
            };

            cartDto.CartDetails = mapper.Map<IEnumerable<CartDetailsDto>>(appDbContext.CartDetails.
                Where(c => c.CardHeaderId == cartDto.CartHeader.CartHeaderId));

            var productDtos = await productService.GetAllProductsAsync();

            foreach (var item in cartDto.CartDetails)
            {
                item.Product = productDtos.FirstOrDefault(p => p.ProductId == item.ProductId);
                cartDto.CartHeader.CartTotal += item.Count * item.Product.Price;
            }

            _response.Results = cartDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> ApplyCouponAsync(CartDto cartDto)
    {
        try
        {
            var cartFromDb = await appDbContext.CartHeaders.FirstAsync(c => c.UserId == cartDto.CartHeader.UserId);
            cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
            appDbContext.CartHeaders.Update(cartFromDb);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> RemoveCouponAsync(CartDto cartDto)
    {
        try
        {
            var cartFromDb = await appDbContext.CartHeaders.FirstAsync(c => c.UserId == cartDto.CartHeader.UserId);
            cartFromDb.CouponCode = string.Empty;
            appDbContext.CartHeaders.Update(cartFromDb);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> GetCartAsyncVersionTwo(string userId)
    {
        try
        {
            var cartDto = new CartDto()
            {
                CartHeader = mapper.Map<CartHeaderDto>(appDbContext.CartHeaders.First(c => c.UserId == userId))
            };

            cartDto.CartDetails = mapper.Map<IEnumerable<CartDetailsDto>>(appDbContext.CartDetails.
                Where(c => c.CardHeaderId == cartDto.CartHeader.CartHeaderId));

            foreach (var item in cartDto.CartDetails)
            {
                var product = await productService.GetProductByIdAsync(item.ProductId);

                item.Product = product;
                cartDto.CartHeader.CartTotal += item.Count * item.Product.Price;
            }

            _response.Results = cartDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> CartUpsertAsync(CartDto cartDto)
    {
        try
        {
            var cartHeaderFromDb = await appDbContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(header => header.UserId == cartDto.CartHeader.UserId);
            if (cartHeaderFromDb == null)
            {
                // Create header and details
                var cartHeader = mapper.Map<CartHeader>(cartDto.CartHeader);
                await appDbContext.CartHeaders.AddAsync(cartHeader);
                await appDbContext.SaveChangesAsync();

                cartDto.CartDetails.First().CardHeaderId = cartHeader.CartHeaderId;
                await appDbContext.CartDetails.AddAsync(mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                // If header is not null
                // Check if details has same product
                var cartDetailsFromDb = await appDbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(details => details.ProductId == cartDto.CartDetails.First().ProductId &&
                details.CardHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    // Create cart details
                    cartDto.CartDetails.First().CardHeaderId = cartHeaderFromDb.CartHeaderId;
                    await appDbContext.CartDetails.AddAsync(mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await appDbContext.SaveChangesAsync();
                }
                else
                {
                    // Update count in cart details
                    cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                    cartDto.CartDetails.First().CardHeaderId = cartDetailsFromDb.CardHeaderId;
                    cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;

                    appDbContext.CartDetails.Update(mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await appDbContext.SaveChangesAsync();
                }
            }

            _response.Results = cartDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDto> RemoveCartAsync(int cartDetailsId)
    {
        try
        {
            var cartDetailsFromDb = await appDbContext.CartDetails.FirstAsync(c => c.CartDetailsId == cartDetailsId);

            int totalCountOfCartItem = appDbContext.CartDetails.Where(c => c.CardHeaderId == cartDetailsFromDb.CardHeaderId).Count();
            appDbContext.CartDetails.Remove(cartDetailsFromDb);

            if (totalCountOfCartItem == 1)
            {
                var cartHeaderToRemove = await appDbContext.CartHeaders.FirstOrDefaultAsync(c => c.CartHeaderId == cartDetailsFromDb.CardHeaderId);

                appDbContext.CartHeaders.Remove(cartHeaderToRemove);
            }

            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
