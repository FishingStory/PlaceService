using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Implementation;

namespace PlaceService.Api.Extensions;

public static class AddGeometryFactoryExtension
{
    public static IServiceCollection AddGeometryFactory(this IServiceCollection services)
    {
        NtsGeometryServices.Instance = new NtsGeometryServices(
            CoordinateArraySequenceFactory.Instance,
            new PrecisionModel(1000d),
            4326,
            GeometryOverlay.NG,
            new CoordinateEqualityComparer());

        services.AddSingleton(NtsGeometryServices.Instance.CreateGeometryFactory());

        return services;
    }
}