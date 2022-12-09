using System;
using System.Collections.Generic;
using DevPrime.Stack.Foundation;
using Application.Services.Notification.Model;

namespace Application.Interfaces.Services
{
    public interface INotificationService
    {
        void Add(Notification command);
        void Update(Notification command);
        void Delete(Notification command);
        Notification Get(Notification query);
        List<Notification> GetAll(Notification query);
    }
}
