using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["UserRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Name = Session["UserName"];
            return View();
        }
    }
}