using Application.Interfaces.Adapters.State;
using Application.Services.Notification;


namespace Tests_Application
{
    public class NotificationStateMock: INotificationState
    {
         public INotificationRepository Notification { get; set; }
      public NotificationStateMock(){}

        public NotificationStateMock(
               INotificationRepository  notification

           )
        {
               Notification = notification;

        }

    }
}
