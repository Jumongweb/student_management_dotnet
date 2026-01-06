using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Services;
using StudentManagement.Middlewares;
using DotNetEnv;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);



// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Services
builder.Services.AddScoped<StudentService>();

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();
app.Run();
