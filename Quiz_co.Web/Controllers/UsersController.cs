using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz_co.Services.Interfaces;
using Quiz_co.ViewModels;

namespace Quiz_co.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> Get()
        {
            return Ok(_userService.GetAllUsers());
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {

            var result = _userService.Login(model);
            if (result == "Succeeded")
            {

                return Ok(result);
            }
            return NotFound("Username or Password is incorrect!");
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            try
            {
                _userService.Register(model);
                return Ok("Successfully registered user!");
            }catch(Exception e)
            {
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
    }
}