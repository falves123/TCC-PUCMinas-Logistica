using Domain.Aggregates.Payment;

namespace Application.EventHandlers.Payment;

public class OrderGetEventHandler : EventHandler<OrderGet, IPaymentState>
{
    private const string ALIAS_ORDER = "services.order.get.uri";
    private readonly string OrderURI;


    public OrderGetEventHandler(IPaymentState state, IDp dp) : base(state, dp)
    {
        OrderURI = Dp.Settings.Default(ALIAS_ORDER);
    }

    public override dynamic Handle(OrderGet domainEvent)
    {
         var httpParms = new HTTPParameter($"{OrderURI}/order/{domainEvent.OrderId}");

         var serviceResult = Dp.Services.HTTP.DpGet<OrderEventDTO>(httpParms);

         var itens = new List<Item>();

         if (serviceResult.Dp.Status.Equals(200))
         {
             if (serviceResult.Itens is { Count: > 0 })
             {
                 itens.AddRange(serviceResult.Itens.Select(item => new Item(item.ID, item.Description)));
             }
         }

        return itens;
    }
} 