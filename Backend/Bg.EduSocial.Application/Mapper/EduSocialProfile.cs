using AutoMapper;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Submissions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application.Mapper
{
    public class EduSocialProfile : Profile
    {
        public EduSocialProfile() {
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
            CreateMap<QuestionEditDto, Question>();
            CreateMap<Test, TestDto>();
            CreateMap<TestDto, Test>();
            CreateMap<TestInsertDto, Test>();
            CreateMap<TestUpdateDto, Test>();
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();
            CreateMap<AnswerEditDto, Answer>();
            CreateMap<Submission, SubmissionDto>();
            CreateMap<SubmissionEditDto, Submission>();
            CreateMap<Submission, SubmissionEditDto>();
            CreateMap<SubmissionAnswerDto, SubmissionAnswer>();
            CreateMap<SubmissionAnswer, SubmissionAnswerDto>();
            CreateMap<Classroom, ClassroomDto>();
            CreateMap<ClassroomDto, Classroom>();
            CreateMap<ClassroomEditDto, Classroom>();
        }
    }
}
