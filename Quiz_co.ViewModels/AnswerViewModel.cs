using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public QuestionViewModel Question { get; set; }
    }
}
