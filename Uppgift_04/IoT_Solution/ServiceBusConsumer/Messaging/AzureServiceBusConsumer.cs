using Azure.Messaging.ServiceBus;
using IoT_ServiceBusConsumer.Messaging.IMessaging;
using Newtonsoft.Json;
using ServiceBusConsumer.Models.Dtos;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT_ServiceBusConsumer.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        // registration
        private readonly string _registrationconnectionString;
        private readonly string _registrationQueue;    
        private ServiceBusProcessor _registrationProcessor;

        private readonly IConfiguration _configuration;
        public AzureServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            // registration
            _registrationconnectionString = _configuration.GetValue<string>("ServiceBusConnectionString:EmailUserInformationQueue")!;
            _registrationQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailUserInformationQueue")!;
            var client = new ServiceBusClient(_registrationconnectionString);
            _registrationProcessor = client.CreateProcessor(_registrationQueue);

        }

        public async Task Start()
        {
            // Registration
            _registrationProcessor.ProcessMessageAsync += OnRegistrationRequestReceived;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;
            await _registrationProcessor.StartProcessingAsync();

        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnRegistrationRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            RegistrationDto registrationDto = JsonConvert.DeserializeObject<RegistrationDto>(body)!;

            try
            {
                //TODO Skicka mejl till användaren/databas lagring etc
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Stop()
        {
            await _registrationProcessor.StopProcessingAsync();
            await _registrationProcessor.DisposeAsync();
        }
    }
}
