using DevPrime.Stack.Foundation.Application;
using Domain.Aggregates.Notification.Events;
using Application.Services.Notification.Model;
using Application.Interfaces.Adapters.State;

namespace Application.EventHandlers.Notification
{
    public class NotificationUpdatedEventHandler:
        EventHandler<NotificationUpdated,INotificationState>
    {
        public NotificationUpdatedEventHandler(INotificationState state, IDp dp) : base(state, dp) { }
        public override dynamic Handle(NotificationUpdated domainEvent)
        {
            var notification = domainEvent.Get<Domain.Aggregates.Notification.Notification>();
            Dp.State.Notification.Update(notification);

            var destination = Dp.Settings.Default("stream.notificationevents");
            var eventName = "NotificationUpdated";
            var dto = new NotificationUpdatedEventDTO()
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