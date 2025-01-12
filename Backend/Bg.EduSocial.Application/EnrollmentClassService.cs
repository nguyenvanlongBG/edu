using Bg.EduSocial.Application;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Cores;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class EnrollmentClassService : WriteService<IEnrollmentClassRepository, EnrollmentClassEntity, EnrollmentClassDto, EnrollmentClassEditDto>, IEnrollmentClassService
    {
        public EnrollmentClassService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<EnrollmentClassDto>> EnrollmentClassesAsync(Guid classroom_id)
        {
            var filters = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "classroom_id",
                    Operator = FilterOperator.Equal,
                    Value = classroom_id
                }
            };
            var enrolls = await this.FilterAsync(filters);
            if (enrolls?.Count () > 0)
            {

                var filtersUser = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Field = "user_id",
                        Operator = FilterOperator.In,
                        Value = enrolls.Select(e => e.user_id).ToList()
                    }
                };
                var userService = _serviceProvider.GetRequiredService<IUserService>();
                var users = await userService.FilterAsync(filtersUser);
                enrolls.ForEach(e =>
                {
                    var userEnroll = users?.FirstOrDefault(u => u.user_id == e.user_id);
                    e.name = userEnroll?.name ?? string.Empty;
                });
            }
            return enrolls;
        }
    }
}
