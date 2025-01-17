using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.Constants;
using Bg.EduSocial.Domain.Shared.Roles;
using Bg.EduSocial.Helper.Commons;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Bg.EduSocial.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtension
    {
        
        public static void SeedUser(ModelBuilder modelBuilder)
        {
            var users = new List<UserEntity>{ new UserEntity()
                {
                    user_id = Guid.NewGuid(),
                    name = "Admin",
                    user_name = "admin",
                    password = CommonFunction.HashData("Admin@123"),
                    role_id = Role.Admin,
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                } 
            };
            modelBuilder.Entity<UserEntity>().HasData(
                users
            );
        }
        public static void SeedSubject(ModelBuilder modelBuilder)
        {
            var subjects = new List<SubjectEntity> {new SubjectEntity()
                {
                    subject_id = Constant.MathSubjectId,
                    subject_name = "Toán",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                } 
            };
            modelBuilder.Entity<SubjectEntity>().HasData(
                subjects
            );
        }
        public static void SeedChapter(ModelBuilder modelBuilder)
        {
            var chapters = new List<ChapterEntity> {
                new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Hàm số lượng giác và phương trình lượng giác",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                 new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Dãy số cấp số cộng và cấp số nhân",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                 new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Các số đặc trưng đo xu thế trung tâm của mẫu số liệu ghép nhóm",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                 new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Giới hạn. Hàm số liên tục",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                 new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Hàm số mũ và hàm số Logarit",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                  new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Quan hệ vuông góc trong không gian",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                 new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Các quy tắc tính xác suất",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                },
                 new ChapterEntity()
                {
                    chapter_id = Guid.NewGuid(),
                    subject_id = Constant.MathSubjectId,
                    name = "Đạo hàm",
                    created_date = DateTime.Now,
                    modified_date = DateTime.Now,
                }
            };
            modelBuilder.Entity<ChapterEntity>().HasData(
                chapters
            );
        }
    }
}
