using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlaceService.Api.Options;
using PlaceService.Infrastructure.DbContexts;

namespace PlaceService.Api.Extensions;

public static class AddDbContextExtension
{
    public static IServiceCollection AddDbContextWithCustomOptions(this IServiceCollection services)
    {
        services.AddDbContext<PlaceServiceDbContext>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<PlaceServiceDbContextOptions>>().Value;
            client.UseNpgsql(options.DefaultConnection, npgsql => npgsql.UseNetTopologySuite());
        });
        return services;
    }
}