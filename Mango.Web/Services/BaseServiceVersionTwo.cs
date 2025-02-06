using System.Text;
using System.Text.Json;

namespace Mango.Web.Services;

// Here's how to implement the services when using builder.Services.AddHttpClient():
// Register services

// Program.cs
// var builder = WebApplication.CreateBuilder(args);

// Register services
//builder.Services.AddHttpClient();
//builder.Services.AddScoped<IBaseService, BaseService>();
//builder.Services.AddScoped<IOrderService, OrderService>();

// Rest of your Program.cs configuration...

public abstract class BaseServiceVersionTwo
{
    protected readonly IHttpClientFactory _httpClientFactory;

    protected BaseServiceVersionTwo(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected async Task<ResponseDtoVersionTwo?> SendAsync(RequestDtoVersionTwo request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage(
                GetHttpMethod(request.ApiType),
                request.Url)
            {
                Content = request.Data != null
                    ? new StringContent(JsonSerializer.Serialize(request.Data),
                        Encoding.UTF8,
                        "application/json")
                    : null
            };

            var response = await client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            if (response.Content != null)
            {
                var content = await response.Content.ReadFromJsonAsync<ResponseDtoVersionTwo>();
                return content;
            }

            return null;
        }
        catch (HttpRequestException ex)
        {
            // Log the error
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            throw;
        }
    }

    private HttpMethod GetHttpMethod(ApiTypes apiType)
    {
        return apiType switch
        {
            ApiTypes.GET => HttpMethod.Get,
            ApiTypes.POST => HttpMethod.Post,
            ApiTypes.PUT => HttpMethod.Put,
            ApiTypes.DELETE => HttpMethod.Delete,
            _ => throw new ArgumentException("Invalid API type", nameof(apiType))
        };
    }
}

public class RequestDtoVersionTwo
{
    public ApiTypes ApiType { get; set; }
    public string Url { get; set; }
    public object Data { get; set; }
}

public class ResponseDtoVersionTwo
{
    // Add your response properties here
}

// Original Name ApiType, I add 's' to avoid conflict
public enum ApiTypes
{
    GET,
    POST,
    PUT,
    DELETE
}
