using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.ViewModels
{
    public class QuizViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }

        public UserViewModel User { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
