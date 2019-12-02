using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz_co.Services.Interfaces;
using Quiz_co.ViewModels;

namespace Quiz_co.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;
        public QuestionController(IQuestionService questionService, IQuizService quizService)
        {
            _questionService = questionService;
            _quizService = quizService;
        }
        [HttpPost("newquestion")]
        public ActionResult NewQuiz([FromBody] QuestionViewModel model)
        {
            try
            {
                var quiz = _quizService.GetQuizById(model.QuizId);
                model.Quiz = quiz;
                _questionService.CreateQuestion(model);
                var question = _questionService.GetAllQuestions().LastOrDefault();
                return Ok(question);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
    }
}