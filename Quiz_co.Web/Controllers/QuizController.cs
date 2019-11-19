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
    
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IUserService _userService;
        public QuizController(IQuizService quizService, IUserService userService)
        {
            _quizService = quizService;
            _userService = userService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<QuizViewModel>> Get()
        {
            var quizes = _quizService.GetAllQuizzes();
            return Ok(quizes);
        }
        [HttpPost("newquiz")]
        public ActionResult NewQuiz([FromBody] QuizViewModel model)
        {
            UserViewModel user = _userService.GetCurrentUser(User.Identity.Name);
            try
            {
                model.User = user;
                _quizService.CreateQuiz(model);
                return Ok("Successfully added quiz");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
    }
}