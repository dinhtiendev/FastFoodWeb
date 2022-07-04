using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Controllers
{
    public class LogController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
