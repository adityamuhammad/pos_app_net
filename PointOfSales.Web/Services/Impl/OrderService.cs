using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using PointOfSales.Web.Database;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.Models;
using PointOfSales.Web.Services.Contracts;

namespace PointOfSales.Web.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        public OrderService(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }

        public async Task<ResponseDto<OrderCreateResultDto>> CreateOrder(OrderCreateDto orderCreateDto, long userId)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			using var db = _databaseConnectionFactory.GetDbConnection();
		    var orderId = await db.ExecuteScalarAsync<long>(
		        @"insert into orders
                    (user_id, status) 
                  values 
                    (@UserId, 'PENDING_PAYMENT'); 
                  select last_insert_id()", new {UserId = userId});
		    var orderItems = new List<OrderItem>();
		    foreach (var order in orderCreateDto.OrderItems) {
				orderItems.Add(new OrderItem { OrderId = orderId, ProductId = order.ProductId, Quantity = order.Quantity });
			}
		    await db.ExecuteAsync(@"insert into order_item (order_id, product_id, quantity) values (@OrderId, @ProductId, @Quantity)", orderItems);
		    transaction.Complete();
		    return new ResponseDto<OrderCreateResultDto> { Success = true, Message = "Success." };
        }

        public async Task<ResponseDto<List<OrderDetailResultDto>>> GetOrderByUser(long userId)
        {
			using var db = _databaseConnectionFactory.GetDbConnection();
            var orders = await db.QueryAsync<OrderDetailResultDto>(@"
                select id OrderId, status Status 
                from orders 
                where user_id = @UserId", new { UserId = userId });

            var ordersList = orders.ToList();
            var orderIds = ordersList.Select(c => c.OrderId).ToArray();

            var orderDetails = await db.QueryAsync<OrderItemDetailDto>(
                @"select p.id ProductId, oi.order_id OrderId, p.Name ProductName, oi.quantity Quantity
                  from order_item oi join products p on oi.product_id = p.id 
                  where oi.order_id in @OrderIds ", new { OrderIds = orderIds }); ;

            foreach (var order in ordersList) {
                order.OrderDetail = (from orderDetail in orderDetails where orderDetail.OrderId == order.OrderId select orderDetail).ToList();
	        }
            return new ResponseDto<List<OrderDetailResultDto>> { Data = ordersList, Message = "", Success = true };
            
        }
    }
}
