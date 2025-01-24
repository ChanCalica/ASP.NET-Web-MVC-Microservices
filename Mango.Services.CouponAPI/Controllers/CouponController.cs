using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private ResponseDto _response;
    private IMapper _mapper;

    public CouponController(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _response = new ResponseDto();
        _mapper = mapper;
    }

    [HttpGet]
    public ResponseDto GetAllCoupons()
    {
        try
        {
            IEnumerable<Coupon> objList = _dbContext.Coupons.ToList();

            _response.Results = _mapper.Map<IEnumerable<CouponDto>>(objList);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto GetCouponById(int id)
    {
        try
        {
            Coupon obj = _dbContext.Coupons.First(c => c.CouponId == id);

            _response.Results = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("GetCouponByCode/{code}")]
    public ResponseDto GetCouponByCode(string code)
    {
        try
        {
            Coupon obj = _dbContext.Coupons.First(c => c.CouponCode.ToLower() == code.ToLower());

            _response.Results = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPost]
    public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);

            _dbContext.Coupons.Add(obj);

            _dbContext.SaveChanges();

            _response.Results = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPut]
    public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);

            _dbContext.Coupons.Update(obj);

            _dbContext.SaveChanges();

            _response.Results = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpDelete("{id:int}")]
    public ResponseDto DeleteCoupon(int id)
    {
        try
        {
            Coupon obj = _dbContext.Coupons.First(c => c.CouponId == id);
            _dbContext.Coupons.Remove(obj);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}