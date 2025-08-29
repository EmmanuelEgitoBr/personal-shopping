using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Coupon;
using Personal.Shopping.Manager.Web.Services.Interfaces;

namespace Personal.Shopping.Manager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CouponController : Controller
    {
        private readonly ICouponApiClient _couponService;

        public CouponController(ICouponApiClient couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> list = new();

            ResponseDto? response = await _couponService.GetAllCuponsAsync();

            if (response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result!)!)!;
            }

            return View(list);
        }

        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _couponService.CreateCouponAsync(model);

                if (response is not null && response.IsSuccess)
                {
                    TempData["Message"] = "Cupon criado com sucesso!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("CouponIndex");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            CouponDto couponDto = new();

            ResponseDto? response = await _couponService.GetCuponByIdAsync(couponId);

            if (response is not null && response.IsSuccess)
            {
                couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result!)!)!;
                return View(couponDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto model)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(model.CouponId);

            if (response.IsSuccess)
            {
                return RedirectToAction("CouponIndex");
            }

            return View(model);
        }
    }
}
