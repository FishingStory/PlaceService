using NetTopologySuite.Geometries;


namespace PlaceService.Api.Extensions;

public static class AddGeometryFactoryExtension
{
    public static IServiceCollection AddGeometryFactory(this IServiceCollection services)
    {
        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
            NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
            new PrecisionModel(1000d),
            4326, 
            GeometryOverlay.NG,
            new CoordinateEqualityComparer());
        
        services.AddSingleton(NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory());
        
        return services;
    }
}