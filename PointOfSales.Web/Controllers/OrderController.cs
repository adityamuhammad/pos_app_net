using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.RequestPayloads;
using PointOfSales.Web.Services.Contracts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointOfSales.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;


        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var currentUser = base.User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => Int64.Parse(c.Value)).FirstOrDefault();
            var order = _mapper.Map<OrderCreateDto>(orderCreateRequest);
            var createOrder = await _orderService.CreateOrder(order, currentUser);
            if (createOrder.Success) { 
			    return Ok(createOrder);
	        }
            return BadRequest(createOrder);
	    }

        [HttpGet]
        public async Task<IActionResult> GetOrderByUser()
        {
            var currentUser = base.User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => Int64.Parse(c.Value)).FirstOrDefault();
            var orders = await _orderService.GetOrderByUser( currentUser);
            if (orders.Success) { 
			    return Ok(orders);
	        }
            return BadRequest(orders);
	    }
    }
}
