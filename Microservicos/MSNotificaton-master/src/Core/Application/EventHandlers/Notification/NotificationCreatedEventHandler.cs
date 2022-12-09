using DevPrime.Stack.Foundation.Application;
using Domain.Aggregates.Notification.Events;
using Application.Services.Notification.Model;
using Application.Interfaces.Adapters.State;
using Application.Interfaces.Adapters.Tools;
using System.Collections.Generic;

namespace Application.EventHandlers.Notification
{
    public class NotificationCreatedEventHandler:
        EventHandlerWithStateAndTools<NotificationCreated, INotificationState, INotificationTools>
    {
        public NotificationCreatedEventHandler(INotificationState state, IDp dp, INotificationTools nt) : base(state, nt, dp) { }
        public override dynamic Handle(NotificationCreated domainEvent)
        {
            var notification = domainEvent.Get<Domain.Aggregates.Notification.Notification>();
            var parametersList = new List<string>() { notification.ID.ToString() };
            var messageSent = Dp.Tools.WhatsApp.Notification(notification.Name, notification.Number, notification.Parameters)

            if(messageSent)
            { 
                Dp.Observability.Log("Messagem enviada.");
                Dp.Observability.Log("Persistindo notificação.");
                Dp.Stream.Notification.Add(notification);

                var destination = "notificationevents";
                var eventName = "NotificationCreated";

                var dto = new NotificationCreatedEventDTO()
                {
                    Name = notification.Name,
                    Email = notification.Email,
                    Number = notification.Number,
                };

                Dp.Stream.Send(destination, eventName, dto);

                return "Notificação enviada com sucesso";
            }
            //var destination = Dp.Settings.Default("stream.notificationevents");
            //var eventName = "NotificationCreated";
            //var dto = new NotificationCreatedEventDTO()
            //{
            //  Name = notification.Name,
            //  Email = notification.Email,
            //  Number = notification.Number,
            //  ID = notification.ID
            //};    
            //Dp.Stream.Send(destination, eventName, dto);

            

            return true;
        }
    }
}