using Microsoft.OpenApi.Models;
using WaterJugChallenge.BLL.Interfaces;
using WaterJugChallenge.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IWaterJugChallenge, WaterJugChallengeService>();

builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WaterJugChallenge",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
