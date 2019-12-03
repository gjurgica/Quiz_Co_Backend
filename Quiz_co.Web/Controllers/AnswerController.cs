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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        public AnswerController(IAnswerService answerService,IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }
        [HttpPost("newanswer")]
        public ActionResult NewAnswer([FromBody] AnswerViewModel model)
        {
            try
            {
                var question = _questionService.GetQuestionById(model.QuestionId);
                model.Question = question;
                _answerService.CreateAnswer(model);
                return Ok(question.Id);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
    }
}