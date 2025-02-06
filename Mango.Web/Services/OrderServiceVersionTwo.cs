namespace Mango.Web.Services;


// TODO Add IOrderService for Interface of OrderService
public class OrderServiceVersionTwo : BaseServiceVersionTwo/*, IOrderService*/
{
    public OrderServiceVersionTwo(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    // The orderObject is an Object, change the string to class
    public async Task<ResponseDtoVersionTwo?> CreateOrderAsync(string orderObject)
    {
        return await SendAsync(new RequestDtoVersionTwo()
        {
            ApiType = ApiTypes.POST,
            Url = "/api/order",
            Data = orderObject
        });
    }
}


//Key differences from the previous implementation:

// 1. BaseService Changes:
// Uses IHttpClientFactory instead of HttpClient
// Creates clients on demand using _httpClientFactory.CreateClient()
// Maintains the same request/response handling logic
// 2. OrderService Changes:
// Injects IHttpClientFactory instead of HttpClient
// Passes factory to base constructor
// Uses base implementation for HTTP communication
// 3. Registration Benefits:
// More flexible client creation
// Better resource management
// Easier to add named clients later
// Supports Polly policies if needed

//This implementation provides a clean separation of concerns while maintaining all the benefits of proper HTTP client management 1. learn.microsoft.com.The IHttpClientFactory handles the lifecycle of HTTP connections efficiently, preventing common issues like socket exhaustion 2. learn.microsoft.com.


// 1. https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0
// 2. https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests