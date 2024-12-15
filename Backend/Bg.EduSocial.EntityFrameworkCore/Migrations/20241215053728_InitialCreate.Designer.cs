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
    [Migration("20241215053728_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.Property<Guid>("test_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("user_id")
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
                    b.Property<Guid>("questio_chapter_id")
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

                    b.HasKey("questio_chapter_id");

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

                    b.HasKey("user_id");

                    b.ToTable("UserEntity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Bg.EduSocial.Domain.Roles.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.HasDiscriminator().HasValue("Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}