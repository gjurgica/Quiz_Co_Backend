using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Interfaces
{
    public interface IAnswerService
    {
        IEnumerable<AnswerViewModel> GetAllAnswers();
        AnswerViewModel GetAnswerById(int id);
        void CreateAnswer(AnswerViewModel model);
        void DeleteAnswer(int id);
        void UpdateAnswer(AnswerViewModel model);
    }
}
