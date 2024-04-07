using BackEnd.Aplication.Services.Auth;
using BackEnd.Aplication.Services.Authentication;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using BackEnd.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
// Add repositories here
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add new services here
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // TODO: MODIFY SETTINGS
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();