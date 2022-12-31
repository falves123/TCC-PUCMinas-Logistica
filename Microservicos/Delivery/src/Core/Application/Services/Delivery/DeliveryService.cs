namespace Application.Services.Delivery;
public class DeliveryService : ApplicationService<IDeliveryState>, IDeliveryService
{
    public DeliveryService(IDeliveryState state, IDp dp) : base(state, dp)
    {
    }

    public void Add(Model.Delivery command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var delivery = command.ToDomain();
            Dp.Attach(delivery);
            delivery.Add();
        });
    }

    public void Update(Model.Delivery command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var delivery = command.ToDomain();
            Dp.Attach(delivery);
            delivery.Update();
        });
    }

    public void Delete(Model.Delivery command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var delivery = command.ToDomain();
            Dp.Attach(delivery);
            delivery.Delete();
        });
    }

    public PagingResult<IList<Model.Delivery>> GetAll(Model.Delivery query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var delivery = query.ToDomain();
            Dp.Attach(delivery);
            var deliveryList = delivery.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToDeliveryList(deliveryList.Result, deliveryList.Total, query.Offset, query.Limit);
            return result;
        });
    }

    public Model.Delivery Get(Model.Delivery query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var delivery = query.ToDelivery(Dp.State.Delivery.Get(query.ID));
            return delivery;
        });
    }
}