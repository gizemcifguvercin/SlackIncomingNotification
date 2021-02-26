using System.Threading.Tasks;

namespace NotificationConsumer.Handlers
{
   public interface IEventHandler<T>
   {       
      Task Handle(T @event);
   }
}