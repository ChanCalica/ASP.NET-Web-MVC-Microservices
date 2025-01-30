using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
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
            IEnumerable<Coupon> objList = _dbContext.coupons.ToList();

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
            Coupon obj = _dbContext.coupons.First(c => c.CouponId == id);

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
            Coupon obj = _dbContext.coupons.First(c => c.CouponCode.ToLower() == code.ToLower());

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
    [Authorize(Roles = "ADMIN")]
    public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);

            _dbContext.coupons.Add(obj);

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
    [Authorize(Roles = "ADMIN")]
    public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);

            _dbContext.coupons.Update(obj);

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
    [Authorize(Roles = "ADMIN")]
    public ResponseDto DeleteCoupon(int id)
    {
        try
        {
            Coupon obj = _dbContext.coupons.First(c => c.CouponId == id);
            _dbContext.coupons.Remove(obj);
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