using AutoMapper;
using Personal.Shopping.Services.Coupon.Application.Dtos;
using Personal.Shopping.Services.Coupon.Application.Interfaces;
using Personal.Shopping.Services.Coupon.Domain.Interfaces;

namespace Personal.Shopping.Services.Coupon.Application.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;
    private IMapper _mapper;

    public CouponService(ICouponRepository couponRepository, IMapper mapper)
    {
        _couponRepository = couponRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto> GetAllCuponsAsync()
    {
        var coupons = await _couponRepository.GetAllCoupons();

        if(coupons is null || coupons.Count() == 0)
        {
            return new ResponseDto 
            { 
                IsSuccess = false,
                Message = "Não foi possível encontrar os cupons"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<IEnumerable<CouponDto>>(coupons),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> GetCuponByIdAsync(int couponId)
    {
        var coupon = await _couponRepository.GetCouponsById(couponId);

        if (coupon is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar o cupom"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<CouponDto>(coupon),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> GetCuponByCodeAsync(string couponCode)
    {
        var coupon = await _couponRepository.GetCuponByCode(couponCode);

        if (coupon is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar o cupom"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<CouponDto>(coupon),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> CreateCouponAsync(CouponDto coupon)
    {
        coupon.CouponCode = coupon.CouponCode.ToUpper();
        coupon.CreatedAt = DateTime.Now;

        var result = await _couponRepository.CreateCoupon(_mapper.Map<Domain.Entities.Coupon>(coupon));

        if (result is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível criar o cupom"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<CouponDto>(coupon),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> UpdateCouponAsync(CouponDto coupon)
    {
        var result = await _couponRepository.UpdateCoupon(_mapper.Map<Domain.Entities.Coupon>(coupon));

        if (result is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível atualizar o cupom"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<CouponDto>(coupon),
            IsSuccess = true
        };
    }

    public async Task DeleteCouponAsync(int id)
    {
        await _couponRepository.DeleteCoupon(id);
    }
}
