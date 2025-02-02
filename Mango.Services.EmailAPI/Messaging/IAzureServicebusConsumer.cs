namespace Mango.Services.EmailAPI.Messaging;

public interface IAzureServicebusConsumer
{
    Task Start();
    Task Stop();
}
