namespace IoT_ServiceBusConsumer.Messaging.IMessaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
