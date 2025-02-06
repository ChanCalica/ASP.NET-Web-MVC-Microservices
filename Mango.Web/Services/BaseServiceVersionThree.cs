namespace Mango.Web.Services;

public class BaseServiceVersionThree
{
}

// Here's how to implement the services when using builder.Services.AddHttpClient<IBaseService, BaseService>():

// Program.cs
//var builder = WebApplication.CreateBuilder(args);

// Register services
//builder.Services.AddHttpClient<IBaseService, BaseService>();
//builder.Services.AddScoped<IOrderService, OrderService>();

// Rest of your Program.cs configuration...


// The BaseService implementation:

//public abstract class BaseService
//{
//    protected readonly HttpClient _httpClient;

//    protected BaseService(HttpClient httpClient)
//    {
//        _httpClient = httpClient;
//    }

//    protected async Task<ResponseDto?> SendAsync(RequestDto request)
//    {
//        try
//        {
//            var httpRequestMessage = new HttpRequestMessage(
//                GetHttpMethod(request.ApiType),
//                request.Url)
//            {
//                Content = request.Data != null
//                    ? new StringContent(JsonSerializer.Serialize(request.Data),
//                        Encoding.UTF8,
//                        "application/json")
//                    : null
//            };

//            var response = await _httpClient.SendAsync(httpRequestMessage);
//            response.EnsureSuccessStatusCode();

//            if (response.Content != null)
//            {
//                var content = await response.Content.ReadFromJsonAsync<ResponseDto>();
//                return content;
//            }

//            return null;
//        }
//        catch (HttpRequestException ex)
//        {
//            // Log the error
//            Console.WriteLine($"HTTP request failed: {ex.Message}");
//            throw;
//        }
//    }

//    private HttpMethod GetHttpMethod(ApiType apiType)
//    {
//        return apiType switch
//        {
//            ApiType.GET => HttpMethod.Get,
//            ApiType.POST => HttpMethod.Post,
//            ApiType.PUT => HttpMethod.Put,
//            ApiType.DELETE => HttpMethod.Delete,
//            _ => throw new ArgumentException("Invalid API type", nameof(apiType))
//        };
//    }
//}

//public class RequestDto
//{
//    public ApiType ApiType { get; set; }
//    public string Url { get; set; }
//    public object Data { get; set; }
//}

//public class ResponseDto
//{
//    // Add your response properties here
//}

//public enum ApiType
//{
//    GET,
//    POST,
//    PUT,
//    DELETE
//}


// The OrderService implementation:

//public class OrderService : BaseService, IOrderService
//{
//    public OrderService(HttpClient httpClient)
//        : base(httpClient)
//    {
//    }

//    public async Task<ResponseDto?> CreateOrderAsync(CartDto cartDto)
//    {
//        return await SendAsync(new RequestDto()
//        {
//            ApiType = ApiType.POST,
//            Url = "orders",
//            Data = cartDto
//        });
//    }
//}

//Key differences from the previous implementation:

// 1. BaseService Changes:
// Uses HttpClient instead of IHttpClientFactory
// HttpClient is automatically managed by the factory
// Maintains the same request/response handling logic
// 2.OrderService Changes:
// Injects HttpClient instead of IHttpClientFactory
// Passes client to base constructor
// Uses base implementation for HTTP communication
// 3.Registration Benefits:
// More flexible client creation
// Better resource management
// Easier to add named clients later
// Supports Polly policies if needed