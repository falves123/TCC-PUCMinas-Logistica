using Microsoft.Extensions.DependencyInjection;
using DevPrime.Stream;
using DevPrime.State;
using DevPrime.Stack.Foundation.Stream;
using DevPrime.Stack.Foundation;
using DevPrime.Stack.App.Pipeline;
using Application.EventHandlers;
using Application.Interfaces.Adapters.State;
using DevPrime.State.Connections;
using DevPrime.State.States;
using DevPrime.State.Repositories.Notification;
using Application.Interfaces.Services;
using Application.Services.Notification;


namespace App
{
    public class App : AppBase
    {
        public App()
        {
            AppName = "MSNotification";
        }

        public override void GetDependencies(IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationState, NotificationState>();


            services.AddSingleton<IEventStream, EventStream>();
            services.AddSingleton<IEventHandler, EventHandler>();

        }
        public override void Start()
        {

        }
        public override  void Stop()
        {

        }
    }
}
