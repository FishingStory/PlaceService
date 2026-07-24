using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlaceService.Api.Options;
using PlaceService.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

PlaceServiceDbContextOptions options = new();
builder.Configuration.GetSection("Location").Bind(options);

builder.Services.AddDbContextWithCustomOptions(options);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.Run();

