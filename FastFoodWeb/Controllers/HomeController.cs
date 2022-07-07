
using FastFoodWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                foods = context.Foods.Where(x => x.CategoryId == 1).Take(5).ToList();
            }
            return View(foods);
        }

        public IActionResult Shop(int id, int page)
        {
            List<Category> categories = new List<Category>();
            List<Food> foods = new List<Food>();
            int maxPage = 1;
            using (var context = new FastFoodContext())
            {
                categories = context.Categories.ToList();
                if (id == 0)
                {
                    foods = context.Foods.Skip((page - 1) * 6).Take(6).ToList();
                    maxPage = context.Foods.ToList().Count / 6 + 1;
                }
                else {
                    foods = context.Foods.Where(x => x.CategoryId == id).Skip((page - 1) * 6).Take(6).ToList();
                    maxPage = context.Foods.Where(x => x.CategoryId == id).ToList().Count / 6 + 1;
                }
                
            }
            ViewBag.CategoryId = id;
            ViewBag.MaxPage = maxPage;
            ViewBag.Page = page;
            ViewBag.Categories = categories;
            return View(foods);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
