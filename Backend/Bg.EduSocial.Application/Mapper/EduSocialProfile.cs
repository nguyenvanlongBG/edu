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
            CreateMap<QuestionEntity, QuestionDto>()
            .ForMember(dest => dest.object_content, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.content)
                    ? new List<object>()
                    : JsonSerializer.Deserialize<List<object>>(src.content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<object>()));

            // Map từ QuestionDto sang QuestionEntity
            CreateMap<QuestionDto, QuestionEntity>()
                .ForMember(dest => dest.content, opt => opt.MapFrom(src =>
                    JsonSerializer.Serialize(src.object_content, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    })));

            CreateMap<OptionEditDto, OptionEntity>()
               .ForMember(dest => dest.content, opt => opt.MapFrom(src =>
                   JsonSerializer.Serialize(src.object_content, new JsonSerializerOptions
                   {
                       DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                   })));

            CreateMap<OptionEntity, OptionDto>()
               .ForMember(dest => dest.object_content, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.content)
                    ? new List<object>()
                    : JsonSerializer.Deserialize<List<object>>(src.content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<object>()));
            CreateMap<OptionEntity, OptionEditDto>()
               .ForMember(dest => dest.object_content, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.content)
                    ? new List<object>()
                    : JsonSerializer.Deserialize<List<object>>(src.content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<object>()));
            CreateMap<OptionDto, OptionEditDto>();
            CreateMap<QuestionDto, QuestionEditDto>();
            CreateMap<QuestionEditDto, QuestionDto>();

            CreateMap<ChapterEntity, ChapterDto>();
            CreateMap<ChapterEntity, ChapterEditDto>();
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
