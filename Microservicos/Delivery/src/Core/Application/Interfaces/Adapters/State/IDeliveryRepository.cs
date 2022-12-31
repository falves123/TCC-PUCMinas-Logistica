namespace Application.Interfaces.Adapters.State;
public interface IDeliveryRepository
{
    void Add(Domain.Aggregates.Delivery.Delivery source);
    void Delete(Guid Id);
    void Update(Domain.Aggregates.Delivery.Delivery source);
    Domain.Aggregates.Delivery.Delivery Get(Guid Id);
    List<Domain.Aggregates.Delivery.Delivery> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}