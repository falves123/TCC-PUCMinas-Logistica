using DevPrime.Stack.Web;
using DevPrime.Stack.Foundation.Web;
using Microsoft.AspNetCore.Mvc;

namespace DevPrime.Web.Controllers
{
    public class HomeController : DevPrimeController
    {
        public HomeController(IDpWeb dp) : base(dp) { }
        public IActionResult Index()
        {
            return View();
        }
    }
}
