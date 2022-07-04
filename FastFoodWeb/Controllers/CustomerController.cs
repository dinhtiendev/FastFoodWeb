using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
