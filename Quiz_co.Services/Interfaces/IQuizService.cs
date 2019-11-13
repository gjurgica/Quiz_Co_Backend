using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Interfaces
{
    public interface IQuizService
    {
        IEnumerable<QuizViewModel> GetAllQuizzes();
        QuizViewModel GetQuizById(int id);
        void CreateQuiz(QuizViewModel model);
        void DeleteQuiz(int id);
        void UpdateQuiz(QuizViewModel model);
    }
}
