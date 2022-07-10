﻿using FastFoodWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace FastFoodWeb.Controllers
{
    public class LogController : Controller
    {
        public IActionResult LogIn(string email, string password, bool isSave)
        {
            using (var context = new FastFoodContext())
            {
                Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                if (account != null)
                {
                    HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register(string name, string phone, string email, string password)
        {
            using (var context = new FastFoodContext())
            {
                if (context.Accounts.Any(x => !x.Email.Equals(email)))
                {
                    Account account = new Account { Name = name, Phone = phone, Email = email, Password = password, IsAdmin = false, IsActive = true };
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
