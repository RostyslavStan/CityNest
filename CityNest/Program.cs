using CityNest;
using CityNest.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CityNest.AuthorizationOptions>(builder.Configuration.GetSection(nameof(CityNest.AuthorizationOptions)));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddScoped<IPropertiesServices, PropertiesServices>();
builder.Services.AddScoped<IPropertiesRepository, PropertiesRepository>();

builder.Services.AddSingleton<IJwtProvider, JwtProvider>();


builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<RealStateDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Дозволяє запити з будь-якого джерела
            .AllowAnyMethod()  // Дозволяє будь-який HTTP метод
            .AllowAnyHeader();  // Дозволяє будь-які заголовки
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapGet("/", async context =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});


app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();

