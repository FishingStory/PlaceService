using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlaceService.Infrastructure.DbContexts;

namespace PlaceService.Api.Options;

public static class AddDbContextExtension
{
    public static IServiceCollection AddDbContextWithCustomOptions(this IServiceCollection services, PlaceServiceDbContextOptions placeServiceOptions)
    {
        services.AddDbContext<PlaceServiceDbContext>(options => options.UseNpgsql(placeServiceOptions.DefaultConnection));
        return services;
    }
}