using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Roles;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.Domain.Users;
using Bg.EduSocial.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EntityFrameworkCore.EFCore
{
    public class EduSocialDbContext : IdentityDbContext
    {
        public EduSocialDbContext() { }
        public EduSocialDbContext(DbContextOptions<EduSocialDbContext> options)
        : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<QuestionTest> QuestionTests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionAnswer> SubmissionAnswers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<TestClass> TestClasses { get; set; }
        public DbSet<UserClassroom> UserClassrooms { get; set; }
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
            modelBuilder.Entity<Question>().HasKey(a => a.ID);
            modelBuilder.Entity<Answer>().HasKey(a => a.ID);
            modelBuilder.Entity<Test>().HasKey(a => a.ID);
            modelBuilder.Entity<QuestionTest>().HasKey(a => a.ID);
            modelBuilder.Entity<Submission>().HasKey(a => a.ID);
            modelBuilder.Entity<SubmissionAnswer>().HasKey(a => a.ID);
            modelBuilder.Entity<Classroom>().HasKey(a => a.ID);
            modelBuilder.Entity<TestClass>().HasKey(a => a.ID);
            modelBuilder.Entity<UserClassroom>().HasKey(a => a.ID);
            modelBuilder.Entity<EnrollmentClass>().HasKey(a => a.ID);
            modelBuilder.Entity<Answer>().HasOne(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionID).OnDelete(DeleteBehavior.Cascade);
            ModelBuilderExtension.SeedUser(modelBuilder);
        }
    }
}
