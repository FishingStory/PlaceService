using Microsoft.EntityFrameworkCore;
using PlaceService.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<PlaceServiceDbContext>(
    options => options
        .UseNpgsql(
            builder.Configuration["Location:ConnectionStrings:DefaultConnection"],
            o => o.UseNetTopologySuite()
        ));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.Run();

