using DevPrime.Stack.Foundation.Application;
using Domain.Aggregates.Notification.Events;
using Application.Services.Notification.Model;
using Application.Interfaces.Adapters.State;

namespace Application.EventHandlers.Notification
{
    public class NotificationDeletedEventHandler:
        EventHandler<NotificationDeleted,INotificationState>
    {
        public NotificationDeletedEventHandler(INotificationState state, IDp dp) : base(state, dp) { }
        public override dynamic Handle(NotificationDeleted domainEvent)
        {
            var notification = domainEvent.Get<Domain.Aggregates.Notification.Notification>();
            Dp.State.Notification.Delete(notification.ID);

            var destination = Dp.Settings.Default("stream.notificationevents");
            var eventName = "NotificationDeleted";
            var dto = new NotificationDeletedEventDTO()
            {
              Name = notification.Name,
              Email = notification.Email,
              Number = notification.Number,
              ID = notification.ID
            };    
            Dp.Stream.Send(destination, eventName, dto);

            return true;
        }
    }
}