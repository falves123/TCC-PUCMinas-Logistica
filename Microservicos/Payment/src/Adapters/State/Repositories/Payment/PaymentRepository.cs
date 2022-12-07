namespace DevPrime.State.Repositories.Payment;
public class PaymentRepository : RepositoryBase, IPaymentRepository
{
    public PaymentRepository(IDpState dp, ConnectionEF state) : base(dp)
    {
        ConnectionAlias = "State1";
        State = state;
    }

#region Write

    public void Add(Domain.Aggregates.Payment.Payment payment)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _payment = ToState(payment);
            state.Payment.Add(_payment);
            state.SaveChanges();
        });
    }

    public void Delete(Guid paymentID)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _payment = state.Payment.FirstOrDefault(b => b.PaymentID == paymentID);
            state.Remove(_payment);
            state.SaveChanges();
        });
    }

    public void Update(Domain.Aggregates.Payment.Payment payment)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _payment = ToState(payment);
            state.Update(_payment);
            state.SaveChanges();
        });
    }

#endregion Write

#region Read

    public Domain.Aggregates.Payment.Payment Get(Guid paymentID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var payment = state.Payment.FirstOrDefault(b => b.PaymentID == paymentID);
            var _payment = ToDomain(payment);
            return _payment;
        });
    }

    public List<Domain.Aggregates.Payment.Payment> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            List<Model.Payment> payment = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Payment.Where(GetFilter(filter)).OrderByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    payment = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    payment = result.ToList();
            }
            else
            {
                var result = state.Payment.Where(GetFilter(filter)).OrderBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    payment = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    payment = result.ToList();
            }
            var _payment = ToDomain(payment);
            return _payment;
        });
    }
    private Expression<Func<Model.Payment, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Payment, object>> exp = p => p.PaymentID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "customername")
                exp = p => p.CustomerName;
            else if (field.ToLower() == "orderid")
                exp = p => p.OrderID;
            else if (field.ToLower() == "value")
                exp = p => p.Value;
            else
                exp = p => p.PaymentID;
        }
        return exp;
    }
    private Expression<Func<Model.Payment, bool>> GetFilter(string filter)
    {
        Expression<Func<Model.Payment, bool>> exp = p => true;
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
                    if (field.ToLower() == "customername")
                        exp = p => p.CustomerName.ToLower() == value.ToLower();
                    else if (field.ToLower() == "orderid")
                        exp = p => p.OrderID == new Guid(value);
                    else if (field.ToLower() == "value")
                        exp = p => p.Value == Convert.ToDouble(value);
                    else
                        exp = p => true;
                }
            }
        }
        return exp;
    }

    public bool Exists(Guid paymentID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var payment = state.Payment.Where(x => x.PaymentID == paymentID).FirstOrDefault();
            return (paymentID == payment?.PaymentID);
        });
    }

    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var total = state.Payment.Where(GetFilter(filter)).Count();
            return total;
        });
    }

#endregion Read

#region mappers

    public static DevPrime.State.Repositories.Payment.Model.Payment ToState(Domain.Aggregates.Payment.Payment payment)
    {
        if (payment is null)
            return new DevPrime.State.Repositories.Payment.Model.Payment();
        DevPrime.State.Repositories.Payment.Model.Payment _payment = new DevPrime.State.Repositories.Payment.Model.Payment();
        _payment.PaymentID = payment.ID;
        _payment.CustomerName = payment.CustomerName;
        _payment.OrderID = payment.OrderID;
        _payment.Value = payment.Value;
        return _payment;
    }

    public static Domain.Aggregates.Payment.Payment ToDomain(DevPrime.State.Repositories.Payment.Model.Payment payment)
    {
        if (payment is null)
            return new Domain.Aggregates.Payment.Payment()
            {IsNew = true};
        Domain.Aggregates.Payment.Payment _payment = new Domain.Aggregates.Payment.Payment(payment.PaymentID, payment.CustomerName, payment.OrderID, payment.Value);
        return _payment;
    }

    public static List<Domain.Aggregates.Payment.Payment> ToDomain(IList<DevPrime.State.Repositories.Payment.Model.Payment> paymentList)
    {
        List<Domain.Aggregates.Payment.Payment> _paymentList = new List<Domain.Aggregates.Payment.Payment>();
        if (paymentList != null)
        {
            foreach (var payment in paymentList)
            {
                Domain.Aggregates.Payment.Payment _payment = new Domain.Aggregates.Payment.Payment(payment.PaymentID, payment.CustomerName, payment.OrderID, payment.Value);
                _paymentList.Add(_payment);
            }
        }
        return _paymentList;
    }

#endregion mappers
}