using System;
using System.Threading.Tasks;
using MassTransit;
using NotificationConsumer.Handlers;
using Shared;

namespace NotificationConsumer.Consumers
{
    public class NotificationCreatedConsumer : IConsumer<WeatherForecast>
    {
        public async Task Consume(ConsumeContext<WeatherForecast> context)
        {
            try
            {
                var handler =
                    (NotificationCreatedEventHandler) ServiceManager.ServiceProvider.GetService(
                        typeof(IEventHandler<WeatherForecast>));

                await handler.Handle(context.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}