using System.Net.Http.Json;
using Booket.BuildingBlocks.Application.HttpClients;

namespace Booket.BuildingBlocks.Infrastructure.HttpClients;

public class HttpClientFactoryProvider(
    IHttpClientFactory httpClientFactory)
    : IHttpClientFactoryProvider
{
    public async Task<TResponse> GetAsync<TResponse>(string clientName, string url, string apiKey = null)
    {
        var client = httpClientFactory.CreateClient(clientName);
        if (apiKey != null) client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        return await client.GetFromJsonAsync<TResponse>(url);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string clientName, string url, TRequest content, string apiKey = null)
    {
        var client = httpClientFactory.CreateClient(clientName);
        if (apiKey != null) client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        var response = await client.PostAsJsonAsync(url, content);
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string clientName, string url, TRequest content, string apiKey = null)
    {
        var client = httpClientFactory.CreateClient(clientName);
        if (apiKey != null) client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        var response = await client.PutAsJsonAsync(url, content);
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<bool> DeleteAsync(string clientName, string url, string apiKey = null)
    {
        var client = httpClientFactory.CreateClient(clientName);
        if (apiKey != null) client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        var response = await client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    public void Dispose()
    {
    }
}