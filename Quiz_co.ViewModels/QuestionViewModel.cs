using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int QuizId { get; set; }

        public QuizViewModel Quiz { get; set; }
        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
