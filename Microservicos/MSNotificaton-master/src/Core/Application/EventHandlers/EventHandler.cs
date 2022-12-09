using DevPrime.Stack.Foundation;
using DevPrime.Stack.Foundation.Application;
using Application.EventHandlers.Notification;
using Domain.Aggregates.Notification.Events;
   

namespace Application.EventHandlers
{
    public class EventHandler : IEventHandler
    {
        public EventHandler(IHandler handler)
        {
            handler.Add<NotificationCreated,NotificationCreatedEventHandler>();
            handler.Add<NotificationDeleted,NotificationDeletedEventHandler>();
            handler.Add<NotificationUpdated,NotificationUpdatedEventHandler>();
   
        }
    }
}