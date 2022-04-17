using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PointOfSales.Web.Dtos;

namespace PointOfSales.Web.Services.Contracts
{
    public interface IOrderService
    {
        Task<ResponseDto<OrderCreateResultDto>> CreateOrder(OrderCreateDto orderCreateDto, long userId);
        Task<ResponseDto<List<OrderDetailResultDto>>> GetOrderByUser(long userId);
    }
}
