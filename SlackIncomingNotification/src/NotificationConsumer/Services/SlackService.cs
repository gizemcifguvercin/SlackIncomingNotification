using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NotificationConsumer.Services
{
    public class SlackService : ISlackService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public SlackService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
 
        public async Task<bool> SendNotification(string message)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration.GetSection("SlackNotificationOptions:ClientToken").Value);

            string content = JsonSerializer.Serialize(new {text = message} );

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            return result.IsSuccessStatusCode;
        }
    }
}