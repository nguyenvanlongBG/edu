using AutoMapper;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class QuestionService:  WriteService<Question, QuestionDto, QuestionEditDto, QuestionEditDto>, IQuestionService
    {
        private readonly QuestionManager _questionManager;
        private IQuestionRepository _questionRepository;
        public QuestionService(IUnitOfWork<EduSocialDbContext> unitOfWork ,QuestionManager questionManager, IQuestionRepository questionRepository, IMapper mapper): base(unitOfWork, questionRepository, mapper)
        { 
            _questionManager = questionManager;
            _questionRepository = questionRepository;
        }
    }
}
