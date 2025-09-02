using AutoMapper;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Interfaces;
using Personal.Shopping.Services.Order.Domain.Entity;
using Personal.Shopping.Services.Order.Domain.Interfaces;

namespace Personal.Shopping.Services.Order.Application.Services
{
    public class OrderLogService : IOrderLogService
    {
        protected ResponseDto _response;
        private readonly IOrderLogRepository _orderLogRepository;
        private readonly IMapper _mapper;

        public OrderLogService(IOrderLogRepository orderLogRepository,
            IMapper mapper)
        {
            this._response = new ResponseDto();
            _orderLogRepository = orderLogRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllOrdersAsync()
        {
            try
            {
                var orderLogEntityList = await _orderLogRepository.GetAllOrdersAsync();

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<OrderLogDto>>(orderLogEntityList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> GetAllOrdersByCurrentDayAsync()
        {
            try
            {
                var orderLogEntityList = await _orderLogRepository.GetAllOrdersByCurrentDayAsync();

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<OrderLogDto>>(orderLogEntityList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> GetOrderLogsByIdAsync(long orderLogId)
        {
            try
            {
                var orderLogEntity = await _orderLogRepository.GetOrderLogByIdAsync(orderLogId);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<OrderLogDto>(orderLogEntity);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> GetOrderLogsByOrderHeaderIdAsync(int orderHeaderId)
        {
            try
            {
                var orderLogEntityList = await _orderLogRepository.GetOrderLogsByOrderHeaderIdAsync(orderHeaderId);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<OrderLogDto>>(orderLogEntityList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> GetOrderLogsByUserIdAsync(string userId)
        {
            try
            {
                var orderLogEntityList = await _orderLogRepository.GetOrderLogsByUserIdAsync(userId);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<OrderLogDto>>(orderLogEntityList); 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> CreateOrderLogAsync(OrderLogDto orderLogDto)
        {
            try
            {
                var orderLogEntity = _mapper.Map<OrderLog>(orderLogDto);
                var result = await _orderLogRepository.CreateOrderLogAsync(orderLogEntity);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<OrderLogDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
