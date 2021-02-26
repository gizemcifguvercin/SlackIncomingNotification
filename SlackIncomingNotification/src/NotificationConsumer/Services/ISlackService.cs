using System.Threading.Tasks;

namespace NotificationConsumer.Services
{
    public interface ISlackService
    {
        Task<bool> SendNotification(string message);
    }
}