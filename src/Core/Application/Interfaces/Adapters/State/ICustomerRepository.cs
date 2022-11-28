namespace Application.Interfaces.Adapters.State;
public interface ICustomerRepository
{
    void Add(Domain.Aggregates.Customer.Customer source);
    void Delete(Guid Id);
    void Update(Domain.Aggregates.Customer.Customer source);
    Domain.Aggregates.Customer.Customer Get(Guid Id);
    List<Domain.Aggregates.Customer.Customer> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}