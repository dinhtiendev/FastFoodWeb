using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Wish()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
