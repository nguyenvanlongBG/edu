using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Roles;
using Microsoft.Extensions.DependencyInjection;
using MySqlX.XDevAPI;

namespace Bg.EduSocial.Application
{
    public class ClassroomService : WriteService<IClassroomRepository,ClassroomEntity, ClassroomDto, ClassroomEditDto>, IClassroomService
    {
        public ClassroomService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        /// <summary>
        /// Validate trước khi thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần validate</param>
        /// <returns></returns>
        public override async Task<bool> ValidateBeforeInsert(ClassroomEditDto classroom)
        {
            if (classroom == null || string.IsNullOrEmpty(classroom.name)) return false;
            return true;
        }

        public override async Task HandleBeforeSaveAsync(ClassroomEditDto classroom)
        {
            base.HandleBeforeSaveAsync(classroom);
            classroom.classroom_code = GenerateClassroomCode(classroom.name);
        }

        private async Task<string> GenerateUniqueClassroomCodeAsync(string name)
        {
            string code = string.Empty;
            bool isExistClassroomCode = true;
            while (isExistClassroomCode)
            {
                // Sinh mã tạm thời
                code = GenerateClassroomCode(name);
                var classroomExist = await _repo.GetByClassroomCode(code);
                isExistClassroomCode = classroomExist != null ? true : false;
            }
            return code;
        }
        private string GenerateClassroomCode(string name)
        {
            // Lấy chữ cái đầu từ tên
            string initials = string.Join("", name
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) // Tách các từ
                .Select(word => char.ToUpper(word[0])));

            // Tạo GUID và lấy 8 ký tự cuối
            string guidSuffix = Guid.NewGuid().ToString().Replace("-", "").Substring(24); // Lấy 8 ký tự cuối

            // Ghép chữ cái đầu và GUID
            return $"{initials}-{guidSuffix}".ToUpper(); // Mã in hoa
        }

        public async Task<List<ClassroomDto>> GetClassroomsOfUser()
        {
            var user = contextData.user;
            var filterClass = new List<FilterCondition>();

            if (user.role_id == Role.Teacher || user.role_id == Role.Admin)
            {
                filterClass.Add(new FilterCondition
                {
                    Field = "user_id",
                    Operator = FilterOperator.Equal,
                    Value = user.user_id
                });
                return await this.FilterAsync<ClassroomDto>(filterClass);
            }
            return default;
        }

        public async Task<List<ClassroomDto>> PagingClassroom(PagingParam param)
        {
            var enrollmentService = _serviceProvider.GetRequiredService<IEnrollmentClassService>();
            var classrooms = await this.GetPagingAsync(param);
            var user = contextData.user;
            if (classrooms?.Count > 0 && user != null)
            {
                var filterEnrolls = new List<FilterCondition> {
                    new FilterCondition
                    {
                        Field = "classroom_id",
                        Operator = FilterOperator.In,
                        Value = classrooms.Select(c => c.classroom_id).ToList()
                    },
                    new FilterCondition {
                        Field = "user_id",
                        Operator = FilterOperator.Equal,
                        Value = user.user_id
                    },
                };
                var enrollmens = await enrollmentService.FilterAsync(filterEnrolls);
                classrooms.ForEach(c =>
                {
                    var enroll = enrollmens.FirstOrDefault(e => e.classroom_id == c.classroom_id);
                    if (enroll != null)
                    {
                        c.status = enroll.Status;
                    }
                });
                return classrooms;
            }
            return default;
        }
    }
}
