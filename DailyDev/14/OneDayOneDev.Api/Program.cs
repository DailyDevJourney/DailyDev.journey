using Microsoft.EntityFrameworkCore;
using OneDayOneDev;
using OneDayOneDev.Repository;
using OneDayOneDev.Repository.Interface;
using OneDayOneDev.Service;
using OneDayOneDev.Utils;
using OneDayOneDev.Utils.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IDateTimeProvider,SystemDateTimeProvider>();
builder.Services.AddScoped<FileHandler>();
builder.Services.AddScoped<ILog,Log>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskRules, TaskRules>();



var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
