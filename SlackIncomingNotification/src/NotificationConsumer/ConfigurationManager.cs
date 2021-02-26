using Microsoft.Extensions.Configuration;
using NotificationConsumer.ConfigModel;

namespace NotificationConsumer
{
   public static class ConfigurationManager
    {
        public static RabbitMqConfiguration RabbitMqDefinitions { get; } = new RabbitMqConfiguration();
        public static void BindConfigurations(this IConfiguration config)
        {
            config.Bind("RabbitMqSettings", RabbitMqDefinitions);
        }
    }
}