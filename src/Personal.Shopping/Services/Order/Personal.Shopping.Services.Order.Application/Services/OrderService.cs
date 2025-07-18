using AutoMapper;
using Azure;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Interfaces;
using Personal.Shopping.Services.Order.Application.Utils.Constants;
using Personal.Shopping.Services.Order.Domain.Entity;
using Personal.Shopping.Services.Order.Domain.Interfaces;

namespace Personal.Shopping.Services.Order.Application.Services;

public class OrderService : IOrderService
{
    protected ResponseDto _response;
    private IOrderHeaderRepository _orderHeaderRepository;
    private IOrderDetailRepository _orderDetailRepository;
    private IMapper _mapper;
    
    public OrderService(IOrderHeaderRepository orderHeaderRepository,
                        IOrderDetailRepository orderDetailRepository,
                        IMapper mapper)
    {
        this._response = new ResponseDto();
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailRepository = orderDetailRepository;
        _mapper = mapper;
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
}
