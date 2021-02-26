using System.Threading.Tasks;
using Shared;

namespace NotificationAPI.Services
{
    public interface INotificationService
    {
          Task SendMessageToBus(WeatherForecast @event);
    }
}