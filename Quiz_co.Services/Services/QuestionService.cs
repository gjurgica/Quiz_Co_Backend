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
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IRepository<Question> questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public void CreateQuestion(QuestionViewModel model)
        {
            _questionRepository.Insert(_mapper.Map<Question>(model));
        }

        public void DeleteQuestion(int id)
        {
            _questionRepository.Delete(id);
        }

        public IEnumerable<QuestionViewModel> GetAllQuestions()
        {
            return _mapper.Map<IEnumerable<QuestionViewModel>>(_questionRepository.GetAll());
        }

        public QuestionViewModel GetQuestionById(int id)
        {
            Question model = _questionRepository.GetById(id);

            if (model == null)
                throw new Exception("Question does't exist.");

            return _mapper.Map<QuestionViewModel>(model);
        }

        public void UpdateQuestion(QuestionViewModel model)
        {
            _questionRepository.Update(_mapper.Map<Question>(model));
        }
    }
}
