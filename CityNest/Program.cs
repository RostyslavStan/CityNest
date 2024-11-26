using CityNest;
using CityNest.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddScoped<IPropertiesServices, PropertiesServices>();
builder.Services.AddScoped<IPropertiesRepository, PropertiesRepository>();

builder.Services.AddScoped<IAgentsRepository, AgentsRepository>();

builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

builder.Services.AddDbContext<RealStateDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
            .AllowAnyMethod() 
            .AllowAnyHeader();  
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

