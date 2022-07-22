using FastFoodWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FastFoodWeb.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult List()
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    List<Food> listFood = new List<Food>();
                    using (var context = new FastFoodContext())
                    {
                        ViewBag.ListCategory = context.Categories.ToList();
                        listFood = context.Foods.ToList();
                    }
                    return View(listFood);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Add(Food NewFood)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    using (var context = new FastFoodContext())
                    {
                        context.Foods.Add(NewFood);
                        context.SaveChanges();
                    }
                    return RedirectToAction("List", "Food");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(Food EditFood)
        {
            string? acc = HttpContext.Session.GetString("Account");
            if (acc != null)
            {
                Account account = JsonConvert.DeserializeObject<Account>(acc);
                if (account.IsAdmin == true)
                {
                    using (var context = new FastFoodContext())
                    {
                        context.Foods.Update(EditFood);
                        context.SaveChanges();
                    }
                    return RedirectToAction("List", "Food");
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
                        List<Cart> carts = context.Carts.Where(x => x.FoodId == Id).ToList();
                        if (carts.Count > 0)
                        {
                            foreach (var cart in carts)
                            {
                                context.Carts.Remove(cart);
                            }
                        }
                        List<Wish> wishs = context.Wishs.Where(x => x.FoodId == Id).ToList();
                        if (wishs.Count > 0)
                        {
                            foreach (var wish in wishs)
                            {
                                context.Wishs.Remove(wish);
                            }
                        }
                        List<Order> orders = context.Orders.Where(x => x.FoodId == Id).ToList();
                        if (orders.Count > 0)
                        {
                            foreach (var order in orders)
                            {
                                context.Orders.Remove(order);
                            }
                        }
                        Food food = context.Foods.FirstOrDefault(x => x.Id == Id);
                        if (food != null)
                        {
                            context.Foods.Remove(food);
                        }
                        context.SaveChanges();
                    }
                    return RedirectToAction("List", "Food");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
