using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Interfaces
{
   public interface IQuestionService
    {
        IEnumerable<QuestionViewModel> GetAllQuestions();
        QuestionViewModel GetQuestionById(int id);
        void CreateQuestion(QuestionViewModel model);
        void DeleteQuestion(int id);
        void UpdateQuestion(QuestionViewModel model);
    }
}
