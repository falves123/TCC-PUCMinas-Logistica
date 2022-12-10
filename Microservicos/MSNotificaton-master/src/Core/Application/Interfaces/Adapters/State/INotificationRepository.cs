using System;
using System.Collections.Generic;

namespace Application.Interfaces.Adapters.State
{
    public interface INotificationRepository
    {
        void Add(Domain.Aggregates.Notification.Notification source);
        void Delete(Guid Id);
        void Update(Domain.Aggregates.Notification.Notification source);
        Domain.Aggregates.Notification.Notification Get(Guid Id);
        List<Domain.Aggregates.Notification.Notification> GetAll();
    }
}
