namespace Application.Services.Product;
public class ProductService : ApplicationService<IProductState>, IProductService
{
    public ProductService(IProductState state, IDp dp) : base(state, dp)
    {
    }

    public void Add(Model.Product command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var product = command.ToDomain();
            Dp.Attach(product);
            product.Add();
        });
    }

    public void Update(Model.Product command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var product = command.ToDomain();
            Dp.Attach(product);
            product.Update();
        });
    }

    public void Delete(Model.Product command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var product = command.ToDomain();
            Dp.Attach(product);
            product.Delete();
        });
    }

    public PagingResult<IList<Model.Product>> GetAll(Model.Product query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var product = query.ToDomain();
            Dp.Attach(product);
            var productList = product.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToProductList(productList.Result, productList.Total, query.Offset, query.Limit);
            return result;
        });
    }

    public Model.Product Get(Model.Product query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var product = query.ToProduct(Dp.State.Product.Get(query.ID));
            return product;
        });
    }
}