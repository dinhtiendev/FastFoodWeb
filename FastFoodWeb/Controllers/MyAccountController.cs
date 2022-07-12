using FastFoodWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Order()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                List<Order> orders = new List<Order>();
                using (var context = new FastFoodContext())
                {
                    context.Foods.ToList();
                    orders = context.Orders.Where(x => x.IsActive == true && x.AccountId == account.Id).ToList();
                }
                return View(orders);
            }
            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult ChangeProfile(string name, string email, string phone)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                account.Name = name;
                account.Email = email;
                account.Phone = phone;
                HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));
                using (var context = new FastFoodContext())
                {
                    context.Accounts.Update(account);
                    context.SaveChanges();
                }
                return RedirectToAction("Profile", "MyAccount");
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult ChangePassword(string curPassword, string password)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.Password.Equals(curPassword))
                {
                    account.Password = password;
                    HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));
                    using (var context = new FastFoodContext())
                    {
                        context.Accounts.Update(account);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("Profile", "MyAccount");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
