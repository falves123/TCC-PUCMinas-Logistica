namespace DevPrime.Web;
public class Product : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/product", async (HttpContext http, IProductService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.Product.Model.Product(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/product/{id}", async (HttpContext http, IProductService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.Product.Model.Product(id)), 404));
        app.MapPost("/v1/product", async (HttpContext http, IProductService Service, DevPrime.Web.Models.Product.Product command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/product", async (HttpContext http, IProductService Service, Application.Services.Product.Model.Product command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/product/{id}", async (HttpContext http, IProductService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.Product.Model.Product(id))));
    }
}