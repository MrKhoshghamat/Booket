namespace Booket.BuildingBlocks.Application.Map;

public readonly struct DistanceMatrixInput(
    double originLatitude,
    double originLongitude,
    double destinationLatitude,
    double destinationLongitude)
{
    public double OriginLatitude { get; } = originLatitude;
    public double OriginLongitude { get; } = originLongitude;
    public double DestinationLatitude { get; } = destinationLatitude;
    public double DestinationLongitude { get; } = destinationLongitude;
}