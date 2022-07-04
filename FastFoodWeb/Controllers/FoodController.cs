using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
