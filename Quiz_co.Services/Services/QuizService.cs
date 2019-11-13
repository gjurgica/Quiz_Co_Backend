using AutoMapper;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using Quiz_co.Services.Interfaces;
using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Services
{
    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IMapper _mapper;

        public QuizService(IRepository<Quiz> quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        public void CreateQuiz(QuizViewModel model)
        {
            _quizRepository.Insert(_mapper.Map<Quiz>(model));
        }

        public void DeleteQuiz(int id)
        {
            _quizRepository.Delete(id);
        }

        public IEnumerable<QuizViewModel> GetAllQuizzes()
        {
            return _mapper.Map<IEnumerable<QuizViewModel>>(_quizRepository.GetAll());
        }

        public QuizViewModel GetQuizById(int id)
        {
            Quiz model = _quizRepository.GetById(id);

            if (model == null)
                throw new Exception("Quiz does't exist.");

            return _mapper.Map<QuizViewModel>(model);
        }

        public void UpdateQuiz(QuizViewModel model)
        {
            _quizRepository.Update(_mapper.Map<Quiz>(model));
        }
    }
}
