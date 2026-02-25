using OnedayOneDev_Shared;
using OnedayOneDev_Shared.Repository;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Utils.Interface;
using OnedayOneDev_Shared.Utils;
using OneDayOneDev.Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IDateTimeProvider,SystemDateTimeProvider>();
builder.Services.AddScoped<FileHandler>();
builder.Services.AddScoped<ILog, Log>();
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
