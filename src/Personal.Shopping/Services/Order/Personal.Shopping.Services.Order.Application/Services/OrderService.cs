using AutoMapper;
using Personal.Shopping.Integration.MessageBus.Interfaces;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Dtos.Reward;
using Personal.Shopping.Services.Order.Application.Dtos.Stripe;
using Personal.Shopping.Services.Order.Application.Interfaces;
using Personal.Shopping.Services.Order.Application.Utils.Constants;
using Personal.Shopping.Services.Order.Domain.Entity;
using Personal.Shopping.Services.Order.Domain.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace Personal.Shopping.Services.Order.Application.Services;

public class OrderService : IOrderService
{
    protected ResponseDto _response;
    private IOrderHeaderRepository _orderHeaderRepository;
    private IOrderDetailRepository _orderDetailRepository;
    private IMapper _mapper;
    private readonly IKafkaProducerService<RewardsDto> _kafkaProducerService;

    public OrderService(IOrderHeaderRepository orderHeaderRepository,
                        IOrderDetailRepository orderDetailRepository,
                        IMapper mapper,
                        IKafkaProducerService<RewardsDto> kafkaProducerService)
    {
        this._response = new ResponseDto();
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailRepository = orderDetailRepository;
        _mapper = mapper;
        _kafkaProducerService = kafkaProducerService;
    }

    public async Task<ResponseDto> GetAllOrderHeadersAsync()
    {
        try
        {
            //if(!User.IsInRole("Admin")) { retornar erro - acesso negado }

            var orderHeaderEntitylist = await _orderHeaderRepository.GetAllOrderHeadersAsync();
            var orderHeaderDtolist = _mapper.Map<IEnumerable<OrderHeaderDto>>(orderHeaderEntitylist);

            _response.IsSuccess = true;
            _response.Result = orderHeaderDtolist;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task<ResponseDto> GetOrderHeadersByUserIdAsync(string userId)
    {
        try
        {
            var orderHeaderEntitylist = await _orderHeaderRepository.GetOrderHeadersByUserIdAsync(userId);
            var orderHeaderDtolist = _mapper.Map<IEnumerable<OrderHeaderDto>>(orderHeaderEntitylist);

            _response.IsSuccess = true;
            _response.Result = orderHeaderDtolist;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task<ResponseDto> GetOrderHeaderByIdAsync(int orderHeaderId)
    {
        try
        {
            var orderHeader = await _orderHeaderRepository.GetOrderHeaderByIdAsync(orderHeaderId);
            var orderHeaderDto = _mapper.Map<OrderHeaderDto>(orderHeader);

            _response.IsSuccess = true;
            _response.Result = orderHeaderDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task UpdateOrderHeaderAsync(OrderHeaderDto orderHeaderDto)
    {
        var orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
        await _orderHeaderRepository.UpdateOrderHeaderAsync(orderHeader);
    }

    public async Task<ResponseDto> CreateOrderAsync(CartDto cartDto)
    {
        try
        {
            OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
            orderHeaderDto.OrderTime = DateTime.Now;
            orderHeaderDto.Status = StatusTypes.Status_Pending;
            orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailDto>>(cartDto.CartDetails);
            
            OrderHeader orderHeaderEntity = _mapper.Map<OrderHeader>(orderHeaderDto);
            
            var orderHeaderCreated = await _orderHeaderRepository.CreateCartHeader(orderHeaderEntity);

            orderHeaderDto.OrderHeaderId = orderHeaderCreated.OrderHeaderId;
            _response.Result = orderHeaderDto;
            _response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task<ResponseDto> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto)
    {
        try
        {
            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                LineItems = new List<SessionLineItemOptions>()
            };

            foreach (var item in stripeRequestDto.OrderHeader!.OrderDetails!)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "brl",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product!.Name
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var discountObj = new List<SessionDiscountOptions>()
                {
                    new() {
                        //Coupon = stripeRequestDto.OrderHeader.CouponCode
                        Coupon = "CCePorQG"
                    }
                };

            if (stripeRequestDto.OrderHeader.Discount > 0)
            {
                options.Discounts = discountObj;
            }

            var service = new SessionService();
            Session session = service.Create(options);
            stripeRequestDto.StripeSessionUrl = session.Url;

            var orderHeader = await _orderHeaderRepository.GetOrderHeaderByIdAsync(stripeRequestDto.OrderHeader.OrderHeaderId);
            orderHeader.StripeSessionId = session.Id;

            await _orderHeaderRepository.UpdateOrderHeaderAsync(orderHeader);

            _response.IsSuccess = true;
            _response.Result = stripeRequestDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task<ResponseDto> ValidateStripeSessionAsync(int orderHeaderId)
    {
        try
        {
            var orderHeader = await _orderHeaderRepository.GetOrderHeaderByIdAsync(orderHeaderId);
            
            var service = new SessionService();
            Session session = service.Get(orderHeader.StripeSessionId);

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);


            if (paymentIntent.Status == "succeeded")
            {
                orderHeader.PaymentIntentId = paymentIntent.Id;
                orderHeader.Status = StatusTypes.Status_Approved;
                await _orderHeaderRepository.UpdateOrderHeaderAsync(orderHeader);

                RewardsDto rewardsDto = new()
                {
                    OrderId = orderHeaderId,
                    RewardsActivity = Convert.ToInt32(orderHeader.OrderTotal),
                    UserId = orderHeader.UserId,
                    Email = orderHeader.Email!
                };

                await _kafkaProducerService.PublishOrderAsync(rewardsDto);
            }
            var orderHeaderDto = _mapper.Map<OrderHeaderDto>(orderHeader);

            _response.IsSuccess = true;
            _response.Result = orderHeaderDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task<ResponseDto> UpdateOrderStatusAsync(string newOrderStatus, int orderHeaderId)
    {
        try
        {
            var orderHeader = await _orderHeaderRepository.GetOrderHeaderByIdAsync(orderHeaderId);
            //var orderHeaderDto = _mapper.Map<OrderHeaderDto>(orderHeader);
            orderHeader.Status = newOrderStatus;

            if (newOrderStatus == StatusTypes.Status_Cancelled)
            {
                if(!string.IsNullOrEmpty(orderHeader.PaymentIntentId))
                {
                    var options = new RefundCreateOptions
                    {
                        Reason = RefundReasons.RequestedByCustomer,
                        PaymentIntent = orderHeader.PaymentIntentId
                    };

                    var service = new RefundService();
                    Refund refund = service.Create(options);
                }
            }
            await _orderHeaderRepository.UpdateOrderHeaderAsync(orderHeader);

            _response.IsSuccess = true;
            _response.Result = newOrderStatus;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    } 
}
