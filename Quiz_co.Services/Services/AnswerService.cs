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
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IRepository<Answer> answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }
        public void CreateAnswer(AnswerViewModel model)
        {
            _answerRepository.Insert(_mapper.Map<Answer>(model));
        }

        public void DeleteAnswer(int id)
        {
            _answerRepository.Delete(id);
        }

        public IEnumerable<AnswerViewModel> GetAllAnswers()
        {
            return _mapper.Map<IEnumerable<AnswerViewModel>>(_answerRepository.GetAll());
        }

        public AnswerViewModel GetAnswerById(int id)
        {
            Answer model = _answerRepository.GetById(id);

            if (model == null)
                throw new Exception("Answer does't exist.");

            return _mapper.Map<AnswerViewModel>(model);
        }

        public void UpdateAnswer(AnswerViewModel model)
        {
            _answerRepository.Update(_mapper.Map<Answer>(model));
        }
    }
}
