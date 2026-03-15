using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using OneDayOneDev.Utils;
using OnedayOneDev_Shared;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Repository;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;
using OnedayOneDev_Shared.Utils;
using OnedayOneDev_Shared.Utils.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSection = builder.Configuration.GetSection("Jwt");
var key = jwtSection["Key"]!;
var issuer = jwtSection["Issuer"]!;
var audience = jwtSection["Audience"]!;


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)


        };


    });

builder.Services.AddAuthorization();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "OneDayOneDev API", Version = "v1" });

    
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/api/auth/login", UriKind.Relative),
                Scopes = new Dictionary<string, string>
                {
                    { "api","Acces to API"}
                }
            }
        }
    });


    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("oauth2", document)] = new List<string> { "api" }
    });

});


var dataBasePath = Path.Combine(AppContext.BaseDirectory, "app.db");

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite($"Data Source={dataBasePath}"));




builder.Services.AddSingleton<JwtTokenService>();

builder.Services.AddScoped<IDateTimeProvider,SystemDateTimeProvider>();
builder.Services.AddScoped<FileHandler>();
builder.Services.AddScoped<ILog, Log>();

builder.Services.AddScoped<ITaskService,TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



builder.Services.AddScoped<ITaskRules, TaskRules>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:58455")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var app = builder.Build();
app.UseCors("AngularPolicy");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    db.Database.EnsureCreated();
}


app.UseAuthentication();
app.UseAuthorization();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
