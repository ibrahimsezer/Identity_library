using Identity_library.Data;
using Identity_library.Domain.Helper;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Identity_library.Domain.Service;
using Identity_library.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddSingleton(configuration);

var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton(jwtSettings);


// Add services to the container.
//builder.Services.AddSingleton("SecretKey");
builder.Services.AddDbContext<IdentityDbContext>(_ => _.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//builder.Services.AddScoped<AccountController>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
