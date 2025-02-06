using AutoMapper;
using Mango.Services.OrderAPI.Data;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Models.Dto;
using Mango.Services.OrderAPI.Services.Interfaces;
using Mango.Services.OrderAPI.Utility;

namespace Mango.Services.OrderAPI.Services;

public class OrderService : IOrderService
{
    protected ResponseDto _response;
    private IMapper _mapper;
    private readonly AppDbContext _appDbContext;
    private IProductService _productService;

    public OrderService(IMapper mapper, IProductService productService, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _productService = productService;
        _appDbContext = appDbContext;
        _response = new ResponseDto();
    }

    public async Task<ResponseDto> CreateOrderAsync(CartDto cartDto)
    {
        try
        {
            OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
            orderHeaderDto.OrderTime = DateTime.Now;
            orderHeaderDto.Status = OrderStatus.Status_Pending;
            orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);

            // To use the .Entity and retrieve the PK Id you must use the Add not AddAsync
            //OrderHeader orderEntity =  _appDbContext.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;

            // Map to entity first
            OrderHeader orderEntity = _mapper.Map<OrderHeader>(orderHeaderDto);

            // Add to context
            await _appDbContext.OrderHeaders.AddAsync(orderEntity);

            // Save changes to generate the ID
            await _appDbContext.SaveChangesAsync();

            // Now you can access the ID
            /*int orderId*/
            orderHeaderDto.OrderHeaderId = orderEntity.OrderHeaderId;

            _response.Results = orderHeaderDto;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
