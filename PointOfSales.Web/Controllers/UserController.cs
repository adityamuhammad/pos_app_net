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
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }


        // GET: /<controller>/
        [HttpGet]
        [Route("/login", Name = "LoginPage")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Route("/register", Name = "RegisterPage")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequest userRegisterRequest)
        {
            var user = _mapper.Map<UserRegisterDto>(userRegisterRequest);
            var registeredUser = await _userService.Register(user);
            if (registeredUser.Success)
            {
                return Ok(registeredUser);
            }
            return BadRequest(registeredUser);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest userLoginRequest)
        {
            var user = _mapper.Map<UserLoginDto>(userLoginRequest);
            var loginUser = await _userService.Login(user);
            if (loginUser.Success)
            {
                return Ok(loginUser);
            }
            return BadRequest(loginUser);
        }
    }
}
