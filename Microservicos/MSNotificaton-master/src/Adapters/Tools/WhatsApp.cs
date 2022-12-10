using Application.Interfaces.Adapters.Tools;
using DevPrime.Stack.Foundation;
using DevPrime.Stack.Tools;
using System.Collections.Generic;
using System.Linq;

namespace DevPrime.Tools
{
    public class WhatsApp : DevPrimeTools, IWhatsApp
    {
        public WhatsApp(IDpTools dpTools) : base(dpTools)
        {
        }

        public void Notify(string name, string number, List<string> parameters)
        {
            return Dp.Pipeline(ExecuteResult: () =>
            {
                var message = $"Olá {name}, seja bem vindo ao serviço de notificação via Zap Zap" { parameters.FirstOrDefault() };
                var messageTemplateRequest = new SendMessageTemplateRequest(number, message);
                var paramHttp = new HTTParameter(Dp.Settings.Default("positus.api.notification.url"));
                paramHttp.Content = messageTemplateRequest.ToString();
                paramHttp.Headers.Add("Authorizaton", $"Bearer {Dp.Settings.Default("positus.api.token")}");

                var apiResult = Dp.Services.HTTP.Post<SendMessageTemplateResponse>(paramHttp);

                if (apiResult.Status == 200 && apiResult.Content.message == "The message was sucessfully sent")
                    return true;
                return false;
            });
        }
    }
}
