namespace Application.EventHandlers.Payment.Model;

public class OrderEventDTO : ServicesResult
{
    public Guid OrderID { get; set; }

    public IList<ItensEventDTO> Itens { get; set; }

    public string SKU { get; private set; }
}