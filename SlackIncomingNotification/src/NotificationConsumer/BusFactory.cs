using System;
using System.Linq;
using GreenPipes;
using MassTransit;
using NotificationConsumer.Consumers;

namespace NotificationConsumer
{
    public class BusFactory
    {
        private static IBusControl _bus;
        public static IBusControl Bus => _bus ?? (_bus = CreateUsingRabbitMq());

        private static IBusControl CreateUsingRabbitMq()
        {
            var bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(x =>
            {                
                x.Host(new Uri(ConfigurationManager.RabbitMqDefinitions.Host), h =>
                {
                    h.Username(ConfigurationManager.RabbitMqDefinitions.UserName);
                    h.Password(ConfigurationManager.RabbitMqDefinitions.Password);
                    h.UseCluster(c =>
                    {
                        foreach (var node in ConfigurationManager.RabbitMqDefinitions.Servers.Split(",").ToArray())
                        {
                            c.Node(node);
                        }
                    });
                });

                x.UseConcurrencyLimit(1);

                x.ReceiveEndpoint("Devnot.Events.V1.NotificationCreated", e =>
                {
                    e.PrefetchCount = 1;
                    e.UseRetry(retryConfig =>
                    {
                        retryConfig.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
                    });

                    e.Consumer<NotificationCreatedConsumer>();
                });
            });

            return bus;
        }
    }
}