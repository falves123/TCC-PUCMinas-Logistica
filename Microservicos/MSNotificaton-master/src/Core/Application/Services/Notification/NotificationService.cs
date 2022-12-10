using System.Collections.Generic;
using DevPrime.Stack.Foundation.Application;
using Application.Interfaces.Services;
using Application.Interfaces.Adapters.State;

namespace Application.Services.Notification
{
    public class NotificationService : ApplicationService<INotificationState>, INotificationService
    {
        public NotificationService(INotificationState state, IDp dp) : base(state, dp){}

        public void Add(Model.Notification command)
        {
            Dp.Pipeline(Execute: () =>
            {
                var agg = command.ToNotificationDomain();
                Dp.AddDomain(agg);
                agg.Add();
            });
        }
        public void Update(Model.Notification command)
        {
            Dp.Pipeline(Execute: () =>
            {
                var agg = command.ToNotificationDomain();
                Dp.AddDomain(agg);
                agg.Update();
            });
        }
        public void Delete(Model.Notification command)
        {
            Dp.Pipeline(Execute: () =>
            {
                var agg = command.ToNotificationDomain();
                Dp.AddDomain(agg);
                agg.Delete();
            });
        }
        public List<Model.Notification> GetAll(Model.Notification command)
        {
            return Dp.Pipeline(ExecuteResult: () =>
            {
                var model = command.ToNotificationList(Dp.State.Notification.GetAll());
                return model;
            });
        }
        public Model.Notification Get(Model.Notification command)
        {
            return Dp.Pipeline(ExecuteResult: () =>
            {
                var model = command.ToNotification(Dp.State.Notification.Get(command.ID));
                return model;
            });
        }
    }
}