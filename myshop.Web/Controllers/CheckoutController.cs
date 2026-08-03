using Microsoft.AspNetCore.Mvc;

namespace myshop.Web.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
