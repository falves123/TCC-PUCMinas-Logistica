﻿var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ConnectionEF>(options => options.UseSqlServer(StateAdapter.GetConnection("State1").ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductState, ProductState>();
builder.Services.AddDbContext<ConnectionEF>(options => options.UseSqlServer(StateAdapter.GetConnection("State1").ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddDbContext<ConnectionEF>(options => options.UseSqlServer(StateAdapter.GetConnection("State1").ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IDeliveryState, DeliveryState>();
builder.Services.AddDbContext<ConnectionEF>(options => options.UseSqlServer(StateAdapter.GetConnection("State1").ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddMvc(o =>
{
    o.EnableEndpointRouting = false;
});
builder.Services.AddScoped<IExtensions, Extensions>();
builder.Services.AddScoped<IEventStream, EventStream>();
builder.Services.AddScoped<IEventHandler, Application.EventHandlers.EventHandler>();
await new DpApp(builder).Run("Delivery", (app) =>
{
    app.UseRouting();
    //Uncomment this line to enable Authentication
    app.UseAuthentication();
    DpApp.UseDevPrimeSwagger(app);
    //Uncomment this line to enable UseAuthorization
    app.UseAuthorization();
    app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
}, (builder) =>
{
    DpApp.AddDevPrime(builder.Services);
    DpApp.AddDevPrimeSwagger(builder.Services);
    DpApp.AddDevPrimeSecurity(builder.Services);
});