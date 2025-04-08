using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudApplication.Models;
using static System.Collections.Specialized.BitVector32;
using System.Web.Mvc;

namespace CrudApplication.Controllers
{
    public class AuthController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                TempData["msg"] = "Registration Successful!";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["UserName"] = user.Name;
                Session["UserRole"] = user.Role;

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (user.Role == "User")
                {
                    return RedirectToAction("Home", "User");
                }
                
            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}