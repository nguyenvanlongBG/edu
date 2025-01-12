using AutoMapper;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Chapter;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bg.EduSocial.Application.Mapper
{
    public class EduSocialProfile : Profile
    {
        public EduSocialProfile() {
            CreateMap<QuestionEntity, QuestionDto>();
            CreateMap<QuestionEntity, QuestionEditDto>();
            CreateMap<QuestionEditDto, QuestionDto>();
            CreateMap<QuestionEditDto, QuestionEntity>();
            CreateMap<QuestionDto, QuestionEditDto>();

            // Map từ QuestionDto sang QuestionEntity
            CreateMap<QuestionDto, QuestionEntity>();
            CreateMap<OptionEditDto, OptionEntity>();
            CreateMap<OptionEntity, OptionEditDto>();
            CreateMap<OptionDto, OptionEditDto>();
            CreateMap<OptionEditDto, OptionEntity>();
            CreateMap<OptionEntity, OptionDto>();


            CreateMap<ResultQuestionEditDto, ResultQuestionEntity>();

            CreateMap<ChapterEntity, ChapterDto>();
            CreateMap<ChapterEntity, ChapterEditDto>();
            CreateMap<UserEntity, UserEditDto>();
            CreateMap<UserEntity, UserDto>();
            CreateMap<ClassroomEntity, ClassroomEditDto>();
            CreateMap<ClassroomEntity, ClassroomDto>();
            CreateMap<ClassroomEditDto, ClassroomEntity>();
            CreateMap<QuestionTestEntity, QuestionTestEditDto>();
            CreateMap<QuestionTestEditDto, QuestionTestEntity>();
            CreateMap<TestEntity, TestEditDto>();
            CreateMap<TestEntity, TestDto>();
            CreateMap<ExamEntity, ExamEditDto>();
            CreateMap<ExamEntity, ExamDto>();
            CreateMap<AnswerEntity, AnswerDto>();
            CreateMap<AnswerEntity, AnswerEditDto>();
            CreateMap<ResultQuestionEntity, ResultQuestionEditDto>();
            CreateMap<ResultQuestionEntity, ResultQuestionDto>();
        }
    }
}
