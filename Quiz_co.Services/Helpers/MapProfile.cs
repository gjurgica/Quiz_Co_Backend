using AutoMapper;
using Quiz_co.Domain;
using Quiz_co.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Helpers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Quiz, QuizViewModel>()
                .ForMember(q => q.User, src => src.MapFrom(qv => qv.User))
                .ForMember(q => q.Questions, src => src.MapFrom(qv => qv.Questions))
                .ReverseMap()
                .ForMember(q => q.UserId, src => src.MapFrom(qv => qv.User.Id))
                .ForMember(qv => qv.Id, src => src.Ignore());

            CreateMap<Question, QuestionViewModel>()
                .ForMember(q => q.Quiz, src => src.MapFrom(qv => qv.Quiz))
                .ForMember(q => q.Answers, src => src.MapFrom(qv => qv.Answers))
                .ReverseMap()
                .ForMember(q => q.QuizId, src => src.MapFrom(qv => qv.Quiz.Id))
                .ForMember(q => q.Id, src => src.Ignore());

            CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.Question, src => src.MapFrom(av => av.Question))
                .ReverseMap()
                .ForMember(a => a.QuestionId, src => src.MapFrom(av => av.Question.Id))
                .ForMember(a => a.Id, src => src.Ignore());

            CreateMap<User, UserViewModel>()
               .ReverseMap()
               .ForMember(u => u.EmailConfirmed, src => src.UseValue(true));
            CreateMap<RegisterViewModel, User>()
                .ForMember(u => u.EmailConfirmed, src => src.UseValue(true));
        }
    }
}
