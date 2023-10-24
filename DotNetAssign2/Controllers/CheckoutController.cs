using Microsoft.AspNetCore.Mvc;

namespace DotNetAssign2.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
