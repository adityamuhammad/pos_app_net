using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.RequestPayloads;
using PointOfSales.Web.Services.Contracts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointOfSales.Web.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProductCreateRequest productCreateRequest)
        {
            var product = _mapper.Map<ProductCreateDto>(productCreateRequest);
            var createProduct = await _productService.Create(product);
            if (createProduct.Success) { 
			    return Ok(createProduct);
	        }
            return BadRequest(createProduct);
	    }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]ProductUpdateRequest productUpdateRequest)
        {
            var product = _mapper.Map<ProductUpdateDto>(productUpdateRequest);
            var updateProduct = await _productService.Update(product);
            if (updateProduct.Success) { 
			    return Ok(updateProduct);
	        }
            return BadRequest(updateProduct);
	    }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]ProductDeleteRequest productDeleteRequest)
        {
            await _productService.Delete(productDeleteRequest.Id);
            return NoContent();
		}

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
	    }

        public async Task<IActionResult> GetById([FromQuery]long id)
        {
            var getProduct = await _productService.GetById(id);
            if (getProduct.Success) { 
			    return Ok(getProduct);
	        }
		    return NotFound(getProduct);
	    }
    }
}
