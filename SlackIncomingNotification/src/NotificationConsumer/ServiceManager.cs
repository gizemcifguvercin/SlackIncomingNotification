using System;
using System.Net.Http.Headers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationConsumer.Handlers;
using NotificationConsumer.Services;
using Shared;

namespace NotificationConsumer
{
    public static class ServiceManager
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static void Configure(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddSingleton<IHostedService, MassTransitHostedService>();
            services.AddSingleton<IBus>(_ => BusFactory.Bus);

            services.AddSingleton(typeof(IEventHandler<ViewModel>), typeof(NotificationCreatedEventHandler));

            services.AddScoped<ISlackService, SlackService>();
            services.AddHttpClient();
            services.AddHttpClient<ISlackService,SlackService>(opt =>
            { 
                opt.BaseAddress = new Uri(_configuration.GetSection("SlackNotificationOptions:SlackEndpointUrl").Value);
                opt.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _configuration.GetSection("SlackNotificationOptions:ClientToken").Value);
            });

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}