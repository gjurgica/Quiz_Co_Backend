using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz_co.Services.Interfaces;
using Quiz_co.ViewModels;

namespace Quiz_co.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hosting;
        private readonly string[] ACCEPTED_FILE_TYPES = new string[] { ".jpg", ".jpeg", ".png" };
        public UsersController(IUserService userService, IHostingEnvironment hosting)
        {
            _userService = userService;
            _hosting = hosting;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> Get()
        {
            return Ok(_userService.GetAllUsers());
        }
        [HttpGet("{username}")]
        public ActionResult<UserViewModel> Get(string username)
        {
            return Ok(_userService.GetCurrentUser(username));
        }
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
        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            _userService.Logout();
            return Ok("User is logout");
        }
        [HttpPost]
        public IActionResult Update([FromBody] UserViewModel user)
        {
            _userService.UpdateUser(user);
            return Ok(user);
        }
        [HttpPost("{username}")]
        public IActionResult UploadPhoto(string username,  IFormFile photo)
        {
            try
            {
                var user = _userService.GetCurrentUser(username);
                if (user == null)
                    return NotFound();

                var path = Path.Combine(_hosting.WebRootPath, "usersImages");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                if (photo.Length > 10 * 1024 * 1024)
                    return BadRequest("Maximum file size exceeded");

                if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(photo.FileName)))
                    return BadRequest("Invalid file type");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                var folderName = Path.Combine( "usersImages", fileName).Replace("\\", "/");

                var filePath = Path.Combine(path, fileName);

                photo.CopyTo(new FileStream(filePath, FileMode.Create));

                user.ImageUrl = fileName;
                _userService.UpdateUser(user);

                return Ok(new { folderName });

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}