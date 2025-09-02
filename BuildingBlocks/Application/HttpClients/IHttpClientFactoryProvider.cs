namespace Booket.BuildingBlocks.Application.HttpClients;

public interface IHttpClientFactoryProvider : IDisposable
{
    Task<TResponse> GetAsync<TResponse>(string clientName, string url, string apiKey = null);
    Task<TResponse> PostAsync<TRequest, TResponse>(string clientName, string url, TRequest content, string apiKey = null);
    Task<TResponse> PutAsync<TRequest, TResponse>(string clientName, string url, TRequest content, string apiKey = null);
    Task<bool> DeleteAsync(string clientName, string url, string apiKey = null);
}