using System.Linq;
using System;
using DevPrime.Stack.Foundation.Exceptions;
using Domain.Aggregates.Notification.Events;
using DevPrime.Stack.Foundation;
using System.Collections.Generic;
namespace Domain.Aggregates.Notification
{
  public class Notification : AggRoot
  {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Number { get; private set; }
        public List<string> Parameters { get; private set; }
    
        private void ValidFields()
        {
          if (String.IsNullOrEmpty(Name)) Dp.Notifications.Add("Name is required");
          if (String.IsNullOrEmpty(Email)) Dp.Notifications.Add("Email is required");
          if (String.IsNullOrEmpty(Number)) Dp.Notifications.Add("Number is required");

          Dp.Notifications.ValidateAndThrow();
        }
        public virtual void Add()
        {
          Dp.Pipeline(Execute: () =>
          {
            ValidFields();
            ID = Guid.NewGuid();
            Dp.ProcessEvent(new NotificationCreated());
          });
        }
        public virtual void Update()
        {
          Dp.Pipeline(Execute: () =>
          {
            ValidFields();
            Dp.ProcessEvent(new NotificationUpdated());
          });
        }
        public virtual void Delete()
        {
          Dp.Pipeline(Execute: () =>
          {
            if(ID != Guid.Empty)
            Dp.ProcessEvent(new NotificationDeleted());
          });
        }

  

        public Notification(
            string _Name,
            string _Email,
            string _Number,
            IList<System.String> _Params,
            Guid _ID
        )
        {
            Name = _Name;
            Email = _Email;
            Number = _Number;
            Parameters = _Params?.ToList();
            ID = _ID;

        }
        public Notification(){}

}
}