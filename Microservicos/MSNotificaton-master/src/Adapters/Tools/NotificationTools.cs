using Application.Interfaces.Adapters.Tools;

namespace DevPrime.Tools
{
    public class NotificationTools : INotificationTools
    {
        public IWhatsApp WhatsApp { get;set; }
        public NotificationTools(IWhatsApp whatsapp)
        {
          WhatsApp = whatsapp;
        }
    }
}
