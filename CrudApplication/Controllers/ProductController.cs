using System;
using System.Linq;
using System.Web.Mvc;
using CrudApplication.Models;

public class ProductController : Controller
{
    ApplicationDbContext db = new ApplicationDbContext();

    // Role check before every action
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (Session["UserRole"]?.ToString() != "Admin")
        {
            filterContext.Result = RedirectToAction("Login", "Auth");
            return;
        }
        base.OnActionExecuting(filterContext);
    }

    public ActionResult Index()
    {
        var products = db.Products.ToList();
        return View(products);
    }

   

    public ActionResult Create()
    {
        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public ActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", product.CategoryId); 
        return View(product);
    }

    public ActionResult Edit(int id)
    {
        var product = db.Products.Find(id);
        return View(product);
    }

    [HttpPost]
    public ActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public ActionResult Delete(int id)
    {
        var product = db.Products.Find(id);
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var product = db.Products.Find(id);
        db.Products.Remove(product);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
