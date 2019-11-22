using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (result != null)
            {

                return Ok(result);
            }
            var error = new ErrorViewModel("Username or Password is incorrect!", HttpStatusCode.NotFound);
            return new JsonResult(error) { StatusCode = error.StatusCode };
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            try
            {
                var user = _userService.Register(model);
                return Ok(user);
            }catch(Exception e)
            {
                var error = new ErrorViewModel("Something went wrong. Please contact support!", HttpStatusCode.BadRequest);
                return new JsonResult(error) { StatusCode = error.StatusCode };
            }
        }
    }
}