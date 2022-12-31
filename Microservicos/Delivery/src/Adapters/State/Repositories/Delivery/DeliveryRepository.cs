namespace DevPrime.State.Repositories.Delivery;
public class DeliveryRepository : RepositoryBase, IDeliveryRepository
{
    public DeliveryRepository(IDpState dp, ConnectionEF state) : base(dp)
    {
        ConnectionAlias = "State1";
        State = state;
    }

#region Write

    public void Add(Domain.Aggregates.Delivery.Delivery delivery)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _delivery = ToState(delivery);
            state.Delivery.Add(_delivery);
            state.SaveChanges();
        });
    }

    public void Delete(Guid deliveryID)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _delivery = state.Delivery.FirstOrDefault(b => b.DeliveryID == deliveryID);
            state.Remove(_delivery);
            state.SaveChanges();
        });
    }

    public void Update(Domain.Aggregates.Delivery.Delivery delivery)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _delivery = ToState(delivery);
            state.Update(_delivery);
            state.SaveChanges();
        });
    }

#endregion Write

#region Read

    public Domain.Aggregates.Delivery.Delivery Get(Guid deliveryID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var delivery = state.Delivery.FirstOrDefault(b => b.DeliveryID == deliveryID);
            var _delivery = ToDomain(delivery);
            return _delivery;
        });
    }

    public List<Domain.Aggregates.Delivery.Delivery> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            List<Model.Delivery> delivery = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Delivery.Where(GetFilter(filter)).OrderByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    delivery = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    delivery = result.ToList();
            }
            else
            {
                var result = state.Delivery.Where(GetFilter(filter)).OrderBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    delivery = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    delivery = result.ToList();
            }
            var _delivery = ToDomain(delivery);
            return _delivery;
        });
    }
    private Expression<Func<Model.Delivery, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Delivery, object>> exp = p => p.DeliveryID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "started")
                exp = p => p.Started;
            else if (field.ToLower() == "finished")
                exp = p => p.Finished;
            else if (field.ToLower() == "orderid")
                exp = p => p.OrderID;
            else if (field.ToLower() == "total")
                exp = p => p.Total;
            else
                exp = p => p.DeliveryID;
        }
        return exp;
    }
    private Expression<Func<Model.Delivery, bool>> GetFilter(string filter)
    {
        Expression<Func<Model.Delivery, bool>> exp = p => true;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var slice = filter?.Split("=");
            if (slice.Length > 1)
            {
                var field = slice[0];
                var value = slice[1];
                if (string.IsNullOrWhiteSpace(value))
                {
                    exp = p => true;
                }
                else
                {
                    if (field.ToLower() == "started")
                        exp = p => p.Started == Convert.ToDateTime(value);
                    else if (field.ToLower() == "finished")
                        exp = p => p.Finished == Convert.ToDateTime(value);
                    else if (field.ToLower() == "orderid")
                        exp = p => p.OrderID == new Guid(value);
                    else if (field.ToLower() == "total")
                        exp = p => p.Total == Convert.ToDouble(value);
                    else
                        exp = p => true;
                }
            }
        }
        return exp;
    }

    public bool Exists(Guid deliveryID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var delivery = state.Delivery.Where(x => x.DeliveryID == deliveryID).FirstOrDefault();
            return (deliveryID == delivery?.DeliveryID);
        });
    }

    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var total = state.Delivery.Where(GetFilter(filter)).Count();
            return total;
        });
    }

#endregion Read

#region mappers

    public static DevPrime.State.Repositories.Delivery.Model.Delivery ToState(Domain.Aggregates.Delivery.Delivery delivery)
    {
        if (delivery is null)
            return new DevPrime.State.Repositories.Delivery.Model.Delivery();
        DevPrime.State.Repositories.Delivery.Model.Delivery _delivery = new DevPrime.State.Repositories.Delivery.Model.Delivery();
        _delivery.DeliveryID = delivery.ID;
        _delivery.Started = delivery.Started;
        _delivery.Finished = delivery.Finished;
        _delivery.OrderID = delivery.OrderID;
        _delivery.Total = delivery.Total;
        return _delivery;
    }

    public static Domain.Aggregates.Delivery.Delivery ToDomain(DevPrime.State.Repositories.Delivery.Model.Delivery delivery)
    {
        if (delivery is null)
            return new Domain.Aggregates.Delivery.Delivery()
            {IsNew = true};
        Domain.Aggregates.Delivery.Delivery _delivery = new Domain.Aggregates.Delivery.Delivery(delivery.DeliveryID, delivery.Started, delivery.Finished, delivery.OrderID, delivery.Total);
        return _delivery;
    }

    public static List<Domain.Aggregates.Delivery.Delivery> ToDomain(IList<DevPrime.State.Repositories.Delivery.Model.Delivery> deliveryList)
    {
        List<Domain.Aggregates.Delivery.Delivery> _deliveryList = new List<Domain.Aggregates.Delivery.Delivery>();
        if (deliveryList != null)
        {
            foreach (var delivery in deliveryList)
            {
                Domain.Aggregates.Delivery.Delivery _delivery = new Domain.Aggregates.Delivery.Delivery(delivery.DeliveryID, delivery.Started, delivery.Finished, delivery.OrderID, delivery.Total);
                _deliveryList.Add(_delivery);
            }
        }
        return _deliveryList;
    }

#endregion mappers
}