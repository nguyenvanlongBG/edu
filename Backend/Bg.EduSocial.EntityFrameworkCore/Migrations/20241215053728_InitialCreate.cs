using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bg.EduSocial.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    answerid = table.Column<Guid>(name: "answer_id", type: "char(36)", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    examid = table.Column<Guid>(name: "exam_id", type: "char(36)", nullable: false),
                    point = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    note = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.answerid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chapter",
                columns: table => new
                {
                    chapterid = table.Column<Guid>(name: "chapter_id", type: "char(36)", nullable: false),
                    subjectid = table.Column<Guid>(name: "subject_id", type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapter", x => x.chapterid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "classroom",
                columns: table => new
                {
                    classroomid = table.Column<Guid>(name: "classroom_id", type: "char(36)", nullable: false),
                    classroomcode = table.Column<string>(name: "classroom_code", type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    avatar = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classroom", x => x.classroomid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam",
                columns: table => new
                {
                    examid = table.Column<Guid>(name: "exam_id", type: "char(36)", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    testid = table.Column<Guid>(name: "test_id", type: "char(36)", nullable: false),
                    point = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam", x => x.examid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "option",
                columns: table => new
                {
                    optionquestionid = table.Column<Guid>(name: "option_question_id", type: "char(36)", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option", x => x.optionquestionid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    subjectid = table.Column<Guid>(name: "subject_id", type: "char(36)", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    point = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.questionid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question_chapter",
                columns: table => new
                {
                    questiochapterid = table.Column<Guid>(name: "questio_chapter_id", type: "char(36)", nullable: false),
                    chapterid = table.Column<Guid>(name: "chapter_id", type: "char(36)", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    subjectid = table.Column<Guid>(name: "subject_id", type: "char(36)", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_chapter", x => x.questiochapterid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question_test",
                columns: table => new
                {
                    questiontestid = table.Column<Guid>(name: "question_test_id", type: "char(36)", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    testid = table.Column<Guid>(name: "test_id", type: "char(36)", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_test", x => x.questiontestid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "result_question",
                columns: table => new
                {
                    resultquestionid = table.Column<Guid>(name: "result_question_id", type: "char(36)", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result_question", x => x.resultquestionid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    subjectid = table.Column<Guid>(name: "subject_id", type: "char(36)", nullable: false),
                    subjectname = table.Column<string>(name: "subject_name", type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.subjectid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "test",
                columns: table => new
                {
                    testid = table.Column<Guid>(name: "test_id", type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    starttime = table.Column<DateTime>(name: "start_time", type: "datetime(6)", nullable: false),
                    finishtime = table.Column<DateTime>(name: "finish_time", type: "datetime(6)", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test", x => x.testid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "test_classroom",
                columns: table => new
                {
                    testclassroomid = table.Column<Guid>(name: "test_classroom_id", type: "char(36)", nullable: false),
                    classroomid = table.Column<Guid>(name: "classroom_id", type: "char(36)", nullable: false),
                    testid = table.Column<Guid>(name: "test_id", type: "char(36)", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_classroom", x => x.testclassroomid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.userid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_classroom_classroom_code",
                table: "classroom",
                column: "classroom_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "chapter");

            migrationBuilder.DropTable(
                name: "classroom");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "option");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "question_chapter");

            migrationBuilder.DropTable(
                name: "question_test");

            migrationBuilder.DropTable(
                name: "result_question");

            migrationBuilder.DropTable(
                name: "subject");

            migrationBuilder.DropTable(
                name: "test");

            migrationBuilder.DropTable(
                name: "test_classroom");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
