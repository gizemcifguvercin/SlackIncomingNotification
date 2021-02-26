using System;
using System.Text.Json;
using System.Threading.Tasks;
using NotificationConsumer.Services;
using Shared;

namespace NotificationConsumer.Handlers
{
    public class NotificationCreatedEventHandler : IEventHandler<WeatherForecast>
    {
        private readonly ISlackService _slackService;
        public NotificationCreatedEventHandler(ISlackService slackService)
        {
            _slackService = slackService;
        }
        public async Task Handle(WeatherForecast @event)
        { 
            await _slackService.SendNotification(JsonSerializer.Serialize(@event));
        }
    }
}