using Booket.BuildingBlocks.Application.HttpClients;
using Booket.BuildingBlocks.Application.Map;

namespace Booket.BuildingBlocks.Infrastructure.Map;

public class MapService(
    IHttpClientFactoryProvider httpClientFactoryProvider,
    MapConfiguration mapConfiguration)
    : IMapService
{
    public async Task<DistanceMatrixResult> DistanceMatrix(DistanceMatrixInput input)
    {
        var response = await httpClientFactoryProvider.GetAsync<DistanceMatrixResult>(mapConfiguration.ClientName,
            mapConfiguration.BaseUel + mapConfiguration.Urls[MapServiceUrlsEnum.DistanceMatrix.ToString()] +
            $"?origins=b,{input.OriginLatitude},{input.OriginLongitude}&destinations=c,{input.DestinationLatitude},{input.DestinationLongitude}&sorted=true");

        return response;
    }
}