namespace DevPrime.Web;
public class Delivery : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/delivery", async (HttpContext http, IDeliveryService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.Delivery.Model.Delivery(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/delivery/{id}", async (HttpContext http, IDeliveryService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.Delivery.Model.Delivery(id)), 404));
        app.MapPost("/v1/delivery", async (HttpContext http, IDeliveryService Service, DevPrime.Web.Models.Delivery.Delivery command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/delivery", async (HttpContext http, IDeliveryService Service, Application.Services.Delivery.Model.Delivery command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/delivery/{id}", async (HttpContext http, IDeliveryService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.Delivery.Model.Delivery(id))));
    }
}