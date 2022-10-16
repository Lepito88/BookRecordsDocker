using BookRecords;
using BookRecords.Data;
using BookRecords.Helpers;
using BookRecords.Interfaces;
using BookRecords.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services
//    .AddAuthentication()
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
////});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["AppSettings:JWT_Issuer"],
                ValidAudience = builder.Configuration["AppSettings:JWT_Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["AppSettings:JWT_Secret"]))
            };

        });
builder.Services.AddAuthorization();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEntityRelationShipManagerService, EntityRelationShipManagerService>();



//DATABASE CONNECTIONS

//string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tonil\\Documents\\BookRecords.mdf;";
//string connectionString = builder.Configuration.GetConnectionString("dbConnection");
//var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");


builder.Services.AddDbContext<BookRecordsContext>(
    DbContextOptions => DbContextOptions
        //.UseMySql(builder.Configuration["AppSettings:MySqlConnection"], ServerVersion.AutoDetect(builder.Configuration["AppSettings:MySqlConnection"]))
        .UseMySql(builder.Configuration["AppSettings:QnapMySqlConnection"], ServerVersion.AutoDetect(builder.Configuration["AppSettings:QnapMySqlConnection"]))
        //.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        //.UseSqlServer(connectionString)
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);


builder.Services.AddCors();

builder.Configuration.AddEnvironmentVariables()
                     .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.Use(async (contex, next) =>
   // {
   //     EnvironmentVariables.InitializeEnvironmentVariables();
   //     await next();
   // }
   //);
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.UseHttpsRedirection();


app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
