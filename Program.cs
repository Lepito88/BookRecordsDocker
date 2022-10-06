using BookRecords;
using BookRecords.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
});

//string connectionString = builder.Configuration.GetConnectionString("DATABASE_URL");
//var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");


builder.Services.AddDbContext<BookRecordsContext>(
    DbContextOptions => DbContextOptions
        .UseMySql(Environment.GetEnvironmentVariable("DATABASE_URL"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DATABASE_URL")))
// The following three options help with debugging, but should
// be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Use(async (contex, next) =>
    {
        MyEnvironment.SetMySQLConnention();
        await next();
    }
   );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
