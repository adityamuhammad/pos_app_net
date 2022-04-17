using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MySql.Data.MySqlClient;
using PointOfSales.Web.Controllers;
using PointOfSales.Web.Database;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.Services.Contracts;
using PointOfSales.Web.Services.Impl;
using Xunit;

namespace PointOfSales.Test
{
    public class OrderTest
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly Mock<IMapper> _mapper;
        public OrderTest()
        {
            _orderService = new Mock<IOrderService>();
            _mapper = new Mock<IMapper>();

        }
        [Fact]
        public async Task TestOrderController_GetOrder()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, "1")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            _orderService.Setup(x => x.GetOrderByUser(1)).Returns(FakeOrderTask);
            OrderController ordercontroller = new OrderController(_mapper.Object, _orderService.Object);
            ordercontroller.ControllerContext = new ControllerContext();
            ordercontroller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            IActionResult actionResult = await ordercontroller.GetOrderByUser();
            var okResult = actionResult as OkObjectResult;
            var data = okResult.Value as ResponseDto<List<OrderDetailResultDto>>;
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(FakeOrder().Data.Count, data.Data.Count);
            Assert.True( data.Success);

        }

        public Task<ResponseDto<List<OrderDetailResultDto>>> FakeOrderTask() => Task.FromResult(FakeOrder());

        private static ResponseDto<List<OrderDetailResultDto>> FakeOrder()
        {
            return new ResponseDto<List<OrderDetailResultDto>>{ Data = new List<OrderDetailResultDto> { 
                new OrderDetailResultDto {
                     OrderId = 1,
                     Status = "PENDING_PAYMENT",
                     OrderDetail = new List<OrderItemDetailDto> {
                         new OrderItemDetailDto { OrderId = 1, ProductId = 2, ProductName = "Baju Kebaya", Quantity = 4}
		             }
		        }
	        } , Message = "", Success = true };
	    }
    }
}
