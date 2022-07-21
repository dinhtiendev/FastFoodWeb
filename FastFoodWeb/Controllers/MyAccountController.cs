using FastFoodWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFoodWeb.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Profile()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cart()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public void AddCart(int id)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                using (var context = new FastFoodContext())
                {
                    Cart cart = context.Carts.FirstOrDefault(x => x.AccountId == account.Id && x.FoodId == id);
                    if (cart == null)
                    {
                        cart = new Cart { FoodId = id, AccountId = account.Id, Quantity = 1 };
                        context.Carts.Add(cart);
                    } else
                    {
                        cart.Quantity += 1;
                        context.Carts.Update(cart);
                    }
                    context.SaveChanges();
                    context.Foods.ToList();
                    List<Cart> listCart = context.Carts.Where(x => x.AccountId == account.Id).ToList();
                    HttpContext.Session.SetString("Carts", JsonConvert.SerializeObject(listCart, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
                }
            }
        }

        public IActionResult UpdateCart()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                using (var context = new FastFoodContext())
                {
                    foreach (Cart cart in JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("Carts")))
                    {
                        int quantity = Convert.ToInt32(Request.Form["quantity-" + cart.Id]);
                        cart.Quantity = quantity;
                        try
                        {
                            var a = context.Carts.Update(cart);
                        } catch { }
                        //AsNoTracking()
                    }
                    context.SaveChanges();
                    context.Foods.ToList();
                    List<Cart> listCart = context.Carts.Where(x => x.AccountId == account.Id).ToList();
                    HttpContext.Session.SetString("Carts", JsonConvert.SerializeObject(listCart, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
                    return RedirectToAction("Cart", "MyAccount");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveCart(int id)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null && id != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                using (var context = new FastFoodContext())
                {
                    Cart c = context.Carts.FirstOrDefault(x => x.Id == id);
                    if (c != null)
                    {
                        context.Carts.Remove(c);
                        context.SaveChanges();
                        context.Foods.ToList();
                        List<Cart> listCart = context.Carts.Where(x => x.AccountId == account.Id).ToList();
                        HttpContext.Session.SetString("Carts", JsonConvert.SerializeObject(listCart, Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                        return RedirectToAction("Cart", "MyAccount");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Wish()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                List<Wish> wishs = new List<Wish>();
                using (var context = new FastFoodContext())
                {
                    context.Foods.ToList();
                    wishs = context.Wishs.Where(x => x.AccountId == account.Id).ToList();
                }
                return View(wishs);
            }
            return RedirectToAction("Index", "Home");
            
        }

        [HttpPost]
        public void AddWish(int id)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                using (var context = new FastFoodContext())
                {
                    Wish wish = context.Wishs.FirstOrDefault(x => x.AccountId == account.Id && x.FoodId == id);
                    if (wish == null)
                    {
                        wish = new Wish { FoodId = id, AccountId = account.Id};
                        context.Wishs.Add(wish);
                    }
                    context.SaveChanges();
                }
            }
        }

        public IActionResult RemoveWish(int id)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                using (var context = new FastFoodContext())
                {
                    Wish w = context.Wishs.FirstOrDefault(x => x.Id == id);
                    if (w != null)
                    {
                        context.Wishs.Remove(w);
                        context.SaveChanges();
                        return RedirectToAction("Wish", "MyAccount");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Checkout()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DoCheckout()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
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
