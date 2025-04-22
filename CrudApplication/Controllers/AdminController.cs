using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudApplication.Models;

namespace CrudApplication.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        //public ActionResult Dashboard()
        //{
        //    if (Session["UserRole"]?.ToString() != "Admin")
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }

        //    ViewBag.Name = Session["UserName"];
        //    return View();
        //}

        public ActionResult Dashboard()
        {
            // Only Admin can access
            if (Session["UserRole"]?.ToString() != "Admin")
                return RedirectToAction("Login", "Auth");

            ViewBag.TotalProducts = db.Products.Count();
            ViewBag.TotalCategories = db.Categories.Count();
            ViewBag.Name = Session["UserName"];
            return View();
        }
    }
}