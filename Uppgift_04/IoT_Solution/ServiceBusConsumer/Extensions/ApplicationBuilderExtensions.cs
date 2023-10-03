using IoT_ServiceBusConsumer.Messaging.IMessaging;
using System.Reflection.Metadata;

namespace IoT_ServiceBusConsumer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static IAzureServiceBusConsumer _serviceBusConsumer { get; set; }

        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            _serviceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
            _serviceBusConsumer.Stop();
        }

        private static void OnStart()
        {
            _serviceBusConsumer.Start();
        }
    }
}
