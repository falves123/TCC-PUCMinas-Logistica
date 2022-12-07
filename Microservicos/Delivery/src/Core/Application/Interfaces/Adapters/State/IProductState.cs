namespace Application.Interfaces.Adapters.State;
public interface IProductState
{
    IProductRepository Product { get; set; }
}