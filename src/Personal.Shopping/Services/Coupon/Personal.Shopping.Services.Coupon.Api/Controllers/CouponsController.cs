using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Coupon.Application.Dtos;
using Personal.Shopping.Services.Coupon.Application.Interfaces;

namespace Personal.Shopping.Services.Coupon.Api.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponsController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponsController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAllCoupons()
    {
        var result = await _couponService.GetAllCuponsAsync();

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult> GetCouponById(int id)
    {
        var result = await _couponService.GetCuponByIdAsync(id);

        return Ok(result);
    }

    [HttpGet("get-by-code/{code}")]
    public async Task<ActionResult> GetCouponByCode(string code)
    {
        var result = await _couponService.GetCuponByCodeAsync(code.ToUpper());

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCoupon([FromBody] CouponDto couponDto)
    {
        var result = await _couponService.CreateCouponAsync(couponDto);
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
    {
        var result = await _couponService.UpdateCouponAsync(couponDto);

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteCoupon(int id)
    {
        await _couponService.DeleteCouponAsync(id);

        return Ok();
    }
}
