using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Shared;

namespace NotificationAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private ISendEndpoint _sendEndpoint;

        public NotificationService(IBus bus, IConfiguration configuration)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task SendMessageToBus(ViewModel @event)
        {
            try
            {
                Uri UriBuilder(string server, string vHost)
                {
                    return new Uri($"{server}/{vHost}");
                }

                var uri = UriBuilder(_configuration.GetSection("RabbitMqSettings:Host").Value,
                    "Devnot.Events.V1.NotificationCreated");

                var sendEndpoint = await _bus.GetSendEndpoint(uri);

                await sendEndpoint.Send(@event); 
            }
            catch(Exception e) { 

                Console.WriteLine(e.Message); 
            }
        }
    }
}