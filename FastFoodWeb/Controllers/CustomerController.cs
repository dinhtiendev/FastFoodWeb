using FastFoodWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FastFoodWeb.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult List()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    List<Account> listFood = new List<Account>();
                    using (var context = new FastFoodContext())
                    {
                        listFood = context.Accounts.Where(x => x.IsAdmin == false).ToList();
                    }
                    return View(listFood);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Add(Account NewAccount)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    NewAccount.IsAdmin = false;
                    using (var context = new FastFoodContext())
                    {
                        context.Accounts.Add(NewAccount);
                        context.SaveChanges();
                    }
                    return RedirectToAction("List", "Customer");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(Account EditAccount)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    EditAccount.IsAdmin = false;
                    using (var context = new FastFoodContext())
                    {
                        context.Accounts.Update(EditAccount);
                        context.SaveChanges();
                    }
                    return RedirectToAction("List", "Customer");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int Id)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    using (var context = new FastFoodContext())
                    {
                        List<Cart> carts = context.Carts.Where(x => x.AccountId == Id).ToList();
                        if (carts.Count > 0)
                        {
                            foreach (var cart in carts)
                            {
                                context.Carts.Remove(cart);
                            }
                        }
                        List<Wish> wishs = context.Wishs.Where(x => x.AccountId == Id).ToList();
                        if (wishs.Count > 0)
                        {
                            foreach (var wish in wishs)
                            {
                                context.Wishs.Remove(wish);
                            }
                        }
                        List<Order> orders = context.Orders.Where(x => x.AccountId == Id).ToList();
                        if (orders.Count > 0)
                        {
                            foreach (var order in orders)
                            {
                                context.Orders.Remove(order);
                            }
                        }
                        Account account1 = context.Accounts.FirstOrDefault(x => x.Id == Id);
                        if (account1 != null)
                        {
                            context.Accounts.Remove(account1);
                        }
                        context.SaveChanges();
                    }
                    return RedirectToAction("List", "Customer");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
