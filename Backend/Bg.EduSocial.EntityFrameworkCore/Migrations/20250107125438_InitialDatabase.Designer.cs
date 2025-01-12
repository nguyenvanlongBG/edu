﻿// <auto-generated />
using System;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bg.EduSocial.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(EduSocialDbContext))]
    [Migration("20250107125438_InitialDatabase")]
    partial class InitialDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Bg.EduSocial.Domain.AnswerEntity", b =>
                {
                    b.Property<Guid>("answer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("exam_id")
                        .HasColumnType("char(36)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("point")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("question_id")
                        .HasColumnType("char(36)");

                    b.HasKey("answer_id");

                    b.ToTable("answer");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.ChapterEntity", b =>
                {
                    b.Property<Guid>("chapter_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("subject_id")
                        .HasColumnType("char(36)");

                    b.HasKey("chapter_id");

                    b.ToTable("chapter");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.ClassroomEntity", b =>
                {
                    b.Property<Guid>("classroom_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("classroom_code")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("classroom_id");

                    b.HasIndex("classroom_code")
                        .IsUnique();

                    b.ToTable("classroom");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.ExamEntity", b =>
                {
                    b.Property<Guid>("exam_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("point")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<Guid>("test_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("user_id")
                        .HasColumnType("char(36)");

                    b.HasKey("exam_id");

                    b.ToTable("exam");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.OptionEntity", b =>
                {
                    b.Property<Guid>("option_question_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("question_id")
                        .HasColumnType("char(36)");

                    b.HasKey("option_question_id");

                    b.ToTable("option");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.QuestionChapterEntity", b =>
                {
                    b.Property<Guid>("question_chapter_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("chapter_id")
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("question_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("subject_id")
                        .HasColumnType("char(36)");

                    b.HasKey("question_chapter_id");

                    b.ToTable("question_chapter");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.QuestionEntity", b =>
                {
                    b.Property<Guid>("question_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("point")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("subject_id")
                        .HasColumnType("char(36)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<Guid?>("user_id")
                        .HasColumnType("char(36)");

                    b.HasKey("question_id");

                    b.ToTable("question");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.QuestionTestEntity", b =>
                {
                    b.Property<Guid>("question_test_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("point")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("question_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("test_id")
                        .HasColumnType("char(36)");

                    b.HasKey("question_test_id");

                    b.ToTable("question_test");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.ResultQuestionEntity", b =>
                {
                    b.Property<Guid>("result_question_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("question_id")
                        .HasColumnType("char(36)");

                    b.HasKey("result_question_id");

                    b.ToTable("result_question");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.SubjectEntity", b =>
                {
                    b.Property<Guid>("subject_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("subject_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("subject_id");

                    b.ToTable("subject");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.TestClassroomEntity", b =>
                {
                    b.Property<Guid>("test_classroom_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("classroom_id")
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("test_id")
                        .HasColumnType("char(36)");

                    b.HasKey("test_classroom_id");

                    b.ToTable("test_classroom");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.TestEntity", b =>
                {
                    b.Property<Guid>("test_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("duration")
                        .HasColumnType("double");

                    b.Property<DateTime?>("finish_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("start_time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("test_id");

                    b.ToTable("test");
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.UserEntity", b =>
                {
                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modified_by")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("role_id")
                        .HasColumnType("int");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}