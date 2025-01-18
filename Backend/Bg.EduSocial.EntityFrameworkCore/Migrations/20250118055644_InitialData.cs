using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bg.EduSocial.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
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
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
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
                name: "enrollment_class",
                columns: table => new
                {
                    enrollmentclassid = table.Column<Guid>(name: "enrollment_class_id", type: "char(36)", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    classroomid = table.Column<Guid>(name: "classroom_id", type: "char(36)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollment_class", x => x.enrollmentclassid);
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
                    status = table.Column<int>(type: "int", nullable: false),
                    questionidsattention = table.Column<string>(name: "question_ids_attention", type: "longtext", nullable: false),
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
                name: "exam_note",
                columns: table => new
                {
                    examnoteid = table.Column<Guid>(name: "exam_note_id", type: "char(36)", nullable: false),
                    examid = table.Column<Guid>(name: "exam_id", type: "char(36)", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_note", x => x.examnoteid);
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
                name: "post",
                columns: table => new
                {
                    postid = table.Column<Guid>(name: "post_id", type: "char(36)", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false),
                    groupid = table.Column<Guid>(name: "group_id", type: "char(36)", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.postid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    subjectid = table.Column<Guid>(name: "subject_id", type: "char(36)", nullable: false),
                    level = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<string>(type: "longtext", nullable: false),
                    chapterids = table.Column<string>(name: "chapter_ids", type: "longtext", nullable: true),
                    from = table.Column<int>(type: "int", nullable: false),
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
                    questionchapterid = table.Column<Guid>(name: "question_chapter_id", type: "char(36)", nullable: false),
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
                    table.PrimaryKey("PK_question_chapter", x => x.questionchapterid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question_test",
                columns: table => new
                {
                    questiontestid = table.Column<Guid>(name: "question_test_id", type: "char(36)", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "char(36)", nullable: false),
                    testid = table.Column<Guid>(name: "test_id", type: "char(36)", nullable: false),
                    point = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    content = table.Column<string>(type: "longtext", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
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
                    duration = table.Column<double>(type: "double", nullable: false),
                    starttime = table.Column<DateTime>(name: "start_time", type: "datetime(6)", nullable: false),
                    finishtime = table.Column<DateTime>(name: "finish_time", type: "datetime(6)", nullable: true),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
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
                name: "user",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    username = table.Column<string>(name: "user_name", type: "longtext", nullable: false),
                    password = table.Column<string>(type: "longtext", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "int", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "longtext", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime(6)", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "longtext", nullable: false),
                    modifieddate = table.Column<DateTime>(name: "modified_date", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "chapter",
                columns: new[] { "chapter_id", "created_by", "created_date", "modified_by", "modified_date", "name", "subject_id" },
                values: new object[,]
                {
                    { new Guid("08cb38ea-5786-4963-a7d0-fa35a42ad5da"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2874), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2875), "Các số đặc trưng đo xu thế trung tâm của mẫu số liệu ghép nhóm", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("0b8ab62d-ee23-415c-9eea-a4497c93529b"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2877), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2877), "Giới hạn. Hàm số liên tục", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("196214b4-d7df-49f5-8b09-87ec82576c59"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2880), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2881), "Hàm số mũ và hàm số Logarit", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("44cc456c-116f-4381-84e3-3a04177f5ea1"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2906), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2906), "Các quy tắc tính xác suất", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("62854655-647b-4e13-97cb-d419f9b1e0e6"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2844), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2845), "Hàm số lượng giác và phương trình lượng giác", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("631929e6-cf27-460e-8c2f-a0eab1f66cd5"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2870), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2871), "Dãy số cấp số cộng và cấp số nhân", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("f28b0c31-190c-4ecf-9f29-3e5e994d8a2d"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2909), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2909), "Đạo hàm", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") },
                    { new Guid("f7aac895-7913-40b8-82a5-22c30c7fdf57"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2891), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2891), "Quan hệ vuông góc trong không gian", new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2") }
                });

            migrationBuilder.InsertData(
                table: "subject",
                columns: new[] { "subject_id", "created_by", "created_date", "modified_by", "modified_date", "subject_name" },
                values: new object[] { new Guid("bca0a3a0-4b29-4a94-9b14-5d40b2a0c7c2"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2750), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2752), "Toán" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "user_id", "created_by", "created_date", "modified_by", "modified_date", "name", "password", "role_id", "user_name" },
                values: new object[] { new Guid("ee3feb55-5904-4fc2-bbc6-7c867eca34d3"), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2300), "", new DateTime(2025, 1, 18, 12, 56, 44, 685, DateTimeKind.Local).AddTicks(2317), "Admin", "$2a$11$0WYlkW6cptRox/EhTPwaQ.SpSM5wWEErz4ZqcxdyJGHoIYDfbSgNO", 1, "admin" });

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
                name: "chapter");

            migrationBuilder.DropTable(
                name: "classroom");

            migrationBuilder.DropTable(
                name: "enrollment_class");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "exam_note");

            migrationBuilder.DropTable(
                name: "option");

            migrationBuilder.DropTable(
                name: "post");

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
                name: "user");
        }
    }
}
