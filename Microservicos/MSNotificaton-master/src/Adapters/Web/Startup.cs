using DevPrime.Stack.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevPrime.Web
{
    public class Startup : DevPrimeStartup<Startup>
    {
        public static void Main(string[] args) { }

        public void ConfigureServices(IServiceCollection services)
        {
            //this should always be the first line in this method
            AddDevPrime(services);
            AddDevPrimeSwagger(services);
            AddDevPrimeSecurity(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            //this should always be the first line in this method
            UseDevPrime(app, env, applicationLifetime);
            //Uncomment these lines to enable Authentication
            //app.UseCookiePolicy();
            //app.UseHsts();
            //app.UseHttpsRedirection();
            app.UseRouting();

            //Uncomment this line to enable Authentication
            //app.UseAuthentication();

            UseDevPrimeSwagger(app);

            //Uncomment this line to enable UseAuthorization
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
