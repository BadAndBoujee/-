using DopeZoo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DopeZoo.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult List()
        {
            using (var db = new ShopDbContext())
            {
                var sales = db.Sales
                    .Include(a => a.Author)
                    .ToList();

                return View(sales);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ShopDbContext())
                {
                    var authorId = this.User.Identity.GetUserId();

                    sale.AuthorId = authorId;
                    db.Sales.Add(sale);
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            }

            return View(sale);
        }
    }
}