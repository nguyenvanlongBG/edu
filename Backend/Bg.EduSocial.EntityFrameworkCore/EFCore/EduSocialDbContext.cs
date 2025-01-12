using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Posts;
using Bg.EduSocial.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bg.EduSocial.EntityFrameworkCore.EFCore
{
    public class EduSocialDbContext: DbContext
    {
        private readonly DbContextOptions _options;
        public EduSocialDbContext() { }
        public EduSocialDbContext(DbContextOptions<EduSocialDbContext> options)
        {
            _options = options;
        }
        public DbSet<QuestionEntity> Questions { get; set; }

        public DbSet<OptionEntity> Options { get; set; }

        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<QuestionTestEntity> QuestionTests { get; set; }
        public DbSet<QuestionChapterEntity> QuestionChapters { get; set; }
        public DbSet<ResultQuestionEntity> ResultQuestions { get; set; }
        public DbSet<AnswerEntity> Answers { get; set; }
        public DbSet<ClassroomEntity> Classrooms { get; set; }
        public DbSet<TestClassroomEntity> TestClasses { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<EnrollmentClassEntity> EnrollmentClasses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Bg.EduSocial.Host/"))
             .AddJsonFile("appsettings.json", optional: false);

            // Xây dựng IConfiguration từ ConfigurationBuilder
            IConfiguration configuration = builder.Build();
            if (configuration is not null)
            {
                optionsBuilder.UseMySQL(configuration.GetConnectionString("Default"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(
              entity =>
              {
                  entity.HasKey(u => u.user_id);
              }
          );
            modelBuilder.Entity<ClassroomEntity>(
                entity =>
                {
                    entity.HasKey(u => u.classroom_id);
                    entity.HasIndex(u => u.classroom_code).IsUnique();
                }
            );
            modelBuilder.Entity<SubjectEntity>(
               entity =>
               {
                   entity.HasKey(u => u.subject_id);
               }
           );
            modelBuilder.Entity<SubjectEntity>(
                entity =>
                {
                    entity.HasKey(u => u.subject_id);
                }
            );
            modelBuilder.Entity<ChapterEntity>(
               entity =>
               {
                   entity.HasKey(u => u.chapter_id);
               }
           );
            modelBuilder.Entity<QuestionEntity>(entity =>
            {
                entity.HasKey(u => u.question_id);
            });

            modelBuilder.Entity<OptionEntity>(entity =>
            {
                entity.HasKey(u => u.option_question_id);
            });
            modelBuilder.Entity<ResultQuestionEntity>(entity =>
            {
                entity.HasKey(u => u.result_question_id);
            });
            modelBuilder.Entity<AnswerEntity>(
                entity =>
                {
                    entity.HasKey(u => u.answer_id);
                }
            );
            modelBuilder.Entity<TestEntity>(entity =>
            {
                entity.HasKey(u => u.test_id);
            });
            modelBuilder.Entity<ExamEntity>(entity =>
            {
                entity.HasKey(u => u.exam_id);
            });
            modelBuilder.Entity<QuestionTestEntity>(entity =>
                {
                    entity.HasKey(u => u.question_test_id);
                }
            );
            modelBuilder.Entity<AnswerEntity>(entity =>
            {
                entity.HasKey(u => u.answer_id);
            }
            );
            modelBuilder.Entity<ChapterEntity>(entity =>
            {
                entity.HasKey(u => u.chapter_id);
            }
            );
            modelBuilder.Entity<QuestionChapterEntity>().HasKey(a => a.question_chapter_id);
            //modelBuilder.Entity<Answer>().HasOne(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionID).OnDelete(DeleteBehavior.Cascade);
            ModelBuilderExtension.SeedUser(modelBuilder);
        }
    }
}
