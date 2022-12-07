namespace Application.Interfaces.Adapters.State;
public interface IProductRepository
{
    void Add(Domain.Aggregates.Product.Product source);
    void Delete(Guid Id);
    void Update(Domain.Aggregates.Product.Product source);
    Domain.Aggregates.Product.Product Get(Guid Id);
    List<Domain.Aggregates.Product.Product> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}