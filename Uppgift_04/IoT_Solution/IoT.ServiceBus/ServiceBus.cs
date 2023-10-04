using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IoT.ServiceBus
{
    public class ServiceBus : IServiceBus
    {      
        // Skickar meddelande till min service bus på azure
        public async Task PublishContent(object content, string queue_topic_name, string connectionString)
        {
            await using var client_service_bus = new ServiceBusClient(connectionString);
            ServiceBusSender sender_service_bus = client_service_bus.CreateSender(queue_topic_name);
            var json_content = JsonConvert.SerializeObject(content);
            ServiceBusMessage final_content = new ServiceBusMessage(Encoding.UTF8.GetBytes(json_content))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await sender_service_bus.SendMessageAsync(final_content);
            await client_service_bus.DisposeAsync();
        }
    }
}
