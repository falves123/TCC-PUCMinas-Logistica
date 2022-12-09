using System.Collections.Generic;

namespace Application.Interfaces.Adapters.Tools
{
    public interface IWhatsApp
    {
        void Notify(string name, string number, List<string> parameters);
    }
}
