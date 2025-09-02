namespace Booket.BuildingBlocks.Application.Map;

public interface IMapService
{
    Task<DistanceMatrixResult> DistanceMatrix(
        DistanceMatrixInput input);
}