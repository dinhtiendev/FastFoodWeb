
using FastFoodWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Food> foods = new List<Food>();
            using (var context = new FastFoodContext())
            {
                foods = context.Foods.Where (x => x.IsActive == true && x.CategoryId == 1).Take(5).ToList();
            }
            return View(foods);
        }

        public IActionResult Shop(int id, int page, string info)
        {
            List<Category> categories = new List<Category>();
            List<Food> foods = new List<Food>();
            int maxPage = 1;
            using (var context = new FastFoodContext())
            {
                categories = context.Categories.ToList();
                if (id == 0 && String.IsNullOrWhiteSpace(info))
                {
                    foods = context.Foods.Skip((page - 1) * 6).Take(6).ToList();
                    maxPage = context.Foods.ToList().Count / 6 + 1;
                } else if (id == 0) {
                    foods = context.Foods.Where(x => x.IsActive == true && x.Name.Contains(info)).Skip((page - 1) * 6).Take(6).ToList();
                    maxPage = context.Foods.Where(x => x.IsActive == true && x.Name.Contains(info)).ToList().Count / 6 + 1;
                } else if (String.IsNullOrWhiteSpace(info)) {
                    foods = context.Foods.Where(x => x.IsActive == true && x.CategoryId == id).Skip((page - 1) * 6).Take(6).ToList();
                    maxPage = context.Foods.Where(x => x.IsActive == true && x.CategoryId == id).ToList().Count / 6 + 1;
                } else
                {
                    foods = context.Foods.Where(x => x.IsActive == true && x.CategoryId == id && x.Name.Contains(info)).Skip((page - 1) * 6).Take(6).ToList();
                    maxPage = context.Foods.Where(x => x.IsActive == true && x.CategoryId == id && x.Name.Contains(info)).ToList().Count / 6 + 1;
                }
                
            }
            ViewBag.Info = info;
            ViewBag.CategoryId = id;
            ViewBag.MaxPage = maxPage;
            ViewBag.Page = page;
            ViewBag.Categories = categories;
            return View(foods);
        }

        public IActionResult Details(int id)
        {
            List<Food> relatedFoods = new List<Food>();
            Food food = new Food();
            using (var context = new FastFoodContext())
            {
                food = context.Foods.FirstOrDefault(x => x.Id == id);
                if (food != null)
                {
                    relatedFoods = context.Foods.Where(x => x.IsActive == true && x.CategoryId == food.CategoryId && x.Id != food.Id).Take(3).ToList();
                    string? acc = HttpContext.Session.GetString("Account");
                    if (acc != null)
                    {
                        Account account = JsonConvert.DeserializeObject<Account>(acc);
                        Wish wish = context.Wishs.FirstOrDefault(x => x.AccountId == account.Id && x.FoodId == food.Id);
                        if (wish != null)
                        {
                            ViewBag.IsWish = true;
                        } else
                        {
                            ViewBag.IsWish = false;
                        }
                    }
                    
                }
            }
            if (food == null)
            {
                return NotFound();
            } else
            {
                ViewBag.RelatedFoods = relatedFoods;
                return View(food);
            }
        }
    }
}
