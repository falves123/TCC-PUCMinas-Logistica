using Application.Interfaces.Adapters.State;


namespace DevPrime.State.States
{
    public class NotificationState: INotificationState
    {
        public INotificationRepository Notification { get; set; }

        public NotificationState(
               INotificationRepository  notification

           )
        {
               Notification = notification;

        }

    }
}
