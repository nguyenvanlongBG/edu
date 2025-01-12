using Bg.EduSocial.Application;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Auth;
using Bg.EduSocial.Constract.Chapter;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.FileQuestion;
using Bg.EduSocial.Constract.Posts;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Report;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Chapters;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Exams;
using Bg.EduSocial.Domain.Posts;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Bg.EduSocial.EntityFrameworkCore.Repositories;
using Bg.EduSocial.Host.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
{
    policy.WithOrigins("http://localhost:5173", "http://localhost:5000")
          .AllowAnyMethod()
          .AllowAnyHeader() // Cho phép tất cả các header
          .AllowCredentials(); // Nếu sử dụng cookie hoặc thông tin xác thực;
}));

builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IContextService, ContextService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IFileQuestionService, FileQuestionService>();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<IChapterRepo, ChapterRepo>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IExamRepo, ExamRepo>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IAnswerRepo, AnswerRepo>();
builder.Services.AddScoped<IQuestionTestService, QuestionTestService>();
builder.Services.AddScoped<IQuestionTestRepository, QuestionTestRepository>();
builder.Services.AddScoped<IResultQuestionService, ResultQuestionService>();
builder.Services.AddScoped<IResultQuestionRepo, ResultQuestionRepo>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IOptionService, OptionService>();
builder.Services.AddScoped<IOptionRepo, OptionRepo>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<IConvertService, ConvertService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IEnrollmentClassService, EnrollmentClassService>();
builder.Services.AddScoped<IEnrollmentClassRepository, EnrollmentClassRepo>();
builder.Services.AddDbContext<EduSocialDbContext>(
    options => options.UseMySQL("server=localhost;database=edusocial;user=root;password=")
);
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
//CommonFunction.Initialize(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>(), builder.Services.BuildServiceProvider().GetRequiredService<AuthManager>());
//EditorFunction.Initialize();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
