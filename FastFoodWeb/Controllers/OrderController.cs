using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
