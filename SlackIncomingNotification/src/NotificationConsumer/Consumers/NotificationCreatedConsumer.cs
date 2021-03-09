using System;
using System.Threading.Tasks;
using MassTransit;
using NotificationConsumer.Handlers;
using Shared;

namespace NotificationConsumer.Consumers
{
    public class NotificationCreatedConsumer : IConsumer<ViewModel>
    {
        public async Task Consume(ConsumeContext<ViewModel> context)
        {
            try
            {
                var handler =
                    (NotificationCreatedEventHandler) ServiceManager.ServiceProvider.GetService(
                        typeof(IEventHandler<ViewModel>));

                await handler.Handle(context.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}