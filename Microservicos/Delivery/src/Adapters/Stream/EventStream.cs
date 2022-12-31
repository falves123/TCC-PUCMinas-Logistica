using System;
using Application.Interfaces.Services;
using Application.Services.Delivery.Model;

namespace DevPrime.Stream;
public class EventStream : EventStreamBase, IEventStream
{
    public override void StreamEvents()
    {
        Subscribe<IDeliveryService, PaymentCreatedEventDTO>("Stream1", "PaymentCreated", (dto, paymentService, Dp) =>
        {
            var command = new Delivery()
            {
                ID = Guid.NewGuid(),
                Started = DateTime.Now,
                Finished = DateTime.Now,
                OrderID = dto.OrderID,
                Total = dto.Value
            };

            paymentService.Add(command);
        });
    }
}