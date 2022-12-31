namespace DevPrime.Stream;
public class EventStream : EventStreamBase, IEventStream
{
    public override void StreamEvents()
    {
        Subscribe<IPaymentService, OrderCreatedEventDTO>("Stream1", "OrderCreated", (dto, paymentService, Dp) =>
        {
          var command = new Payment()
          {
              CustomerName = dto.CustomerName,
              OrderID = dto.ID,
              Value = dto.Total
          };

          paymentService.Add(command);        
        });
    }
}