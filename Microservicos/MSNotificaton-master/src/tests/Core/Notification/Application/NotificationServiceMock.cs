using Application.Interfaces.Adapters.State;
using Moq;
using System;
using System.Collections.Generic;
using DevPrime.Stack.Test;
using Domain.Aggregates.Notification.Events;
using DevPrime.Stack.Foundation;
using Application.Services.Notification;

namespace Tests_Application.Notification
{
    public class NotificationModelMock : Application.Services.Notification.Model.Notification
    {
        public override Domain.Aggregates.Notification.Notification ToNotificationDomain()
        {
            var agg = MockNotificationAggRoot(
            this.Name,this.Email,this.Number,this.Params,this.ID            
            );
            return agg;
        }
        public override NotificationModelMock ToNotification(Domain.Aggregates.Notification.Notification agg)
        {
            var model = new NotificationModelMock();
            model.Name = agg.Name;
            model.Email = agg.Email;
            model.Number = agg.Number;
            model.ID = agg.ID;


            return model;
        }
        public override Domain.Aggregates.Notification.Notification ToNotificationDomain(Guid id)
        {
            var agg = MockNotificationAggRoot(
                       String.Empty,
                       String.Empty,
                       String.Empty,
                       null,
                       Guid.Empty

            );
            agg.DevPrimeSetProperty<Guid>("ID", id);

            return agg;
        }
        private Domain.Aggregates.Notification.Notification MockNotificationAggRoot(
            string _Name,
            string _Email,
            string _Number,
            IList<System.String> _Params,
            Guid _ID

        )
        {
            var AggMock = new Mock<Domain.Aggregates.Notification.Notification>(_Name,_Email,_Number,_Params,_ID); ;
            var agg = AggMock.Object;

            AggMock.Setup(o => o.Add()).Callback(() =>
            {
                agg.Dp.ProcessEvent(new NotificationCreated());
            });
            AggMock.Setup(o => o.Update()).Callback(() =>
            {
                agg.Dp.ProcessEvent(new NotificationUpdated());
            });
            AggMock.Setup(o => o.Delete()).Callback(() =>
            {
                agg.Dp.ProcessEvent(new NotificationDeleted());
            });

            return agg;
        }

    }
    public class NotificationServiceMock
    {
        public List<DomainEvent> OutPutDomainEvents { get; set; }

        public Application.Services.Notification.Model.Notification MockCommand()
        {
            var agg = new NotificationModelMock();
                agg.Name = Faker.Lorem.Sentence();
                agg.Email = Faker.Lorem.Sentence();
                agg.Number = Faker.Lorem.Sentence();

            return agg;
        }

        public NotificationService MockNotificationService()
        {
            OutPutDomainEvents = new List<DomainEvent>();

            var state = new NotificationStateMock();
            var dp = new DpMock<INotificationState>(state,(domainevent)=> {
                OutPutDomainEvents.Add(domainevent);
            });
            
            var srv = new NotificationService(state,dp);
            
            return srv;
        }
    }
   
}