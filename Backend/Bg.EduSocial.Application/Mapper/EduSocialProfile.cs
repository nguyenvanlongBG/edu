using AutoMapper;
using Bg.EduSocial.Constract;
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

        }
    }
}
