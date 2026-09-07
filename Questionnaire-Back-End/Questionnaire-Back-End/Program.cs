using Microsoft.EntityFrameworkCore;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Core.Repositories;
using Questionnaire_Back_End.Core.Services;
using Questionnaire_Back_End.Data.DbContext;
using Questionnaire_Back_End.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext, QuestionnaireDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IParticipantAnswerService, ParticipantAnswerService>();
builder.Services.AddScoped<IParticipantAnswerRepository, ParticipantAnswerRepository>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();
builder.Services.AddScoped<ITargetGroupRepository, TargetGroupRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://172.16.4.8:8080", "http://172.16.4.8:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuestionnaireDbContext>();
    context.Questionnaires.FirstOrDefault();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowAngular");

//app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();