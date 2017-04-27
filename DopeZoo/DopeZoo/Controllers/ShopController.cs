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
        public ActionResult Create(Sale model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ShopDbContext())
                {
                    var authorId = this.User.Identity.GetUserId();

                    model.AuthorId = authorId;

                    if (image != null)
                    {
                        var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

                        if (allowedContentTypes.Contains(image.ContentType))
                        {
                            var imagesPath = "/Content/UserImages/";

                            var filename = image.FileName;

                            var uploadPath = imagesPath + filename;

                            var physicalPath = Server.MapPath(uploadPath);

                            image.SaveAs(physicalPath);

                            model.ImagePath = uploadPath;

                        }
                    }

                    db.Sales.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new ShopDbContext())
            {
                var sale = db.Sales.Find(id);

                if (sale == null || !IsAuthorized(sale))
                {
                    return HttpNotFound();
                }

                return View(sale);
            }
        }

        [Authorize]
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            using (var db = new ShopDbContext())
            {
                var sale = db.Sales.Find(id);

                if (sale == null || !IsAuthorized(sale))
                {
                    return HttpNotFound();
                }

                db.Sales.Remove(sale);
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new ShopDbContext())
            {
                var sale = db.Sales.Find(id);

                if (sale == null || !IsAuthorized(sale))
                {
                    return HttpNotFound();
                }

                var saleViewModel = new SaleViewModel
                {
                    Id = sale.Id,
                    Title = sale.Title,
                    Price = sale.Price,
                    Content = sale.Content,
                    ImagePath = sale.ImagePath,
                    AuthorId = sale.AuthorId
                };

                return View(saleViewModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(SaleViewModel model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ShopDbContext())
                {
                    var sale = db.Sales.Find(model.Id);

                    if (sale == null || !IsAuthorized(sale))
                    {
                        return HttpNotFound();
                    }

                    if (sale == null || !IsAuthorized(sale))
                    {
                        return HttpNotFound();
                    }

                    sale.Title = model.Title;
                    sale.Price = model.Price;
                    sale.Content = model.Content;

                    if (image != null)
                    {
                        var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

                        if (allowedContentTypes.Contains(image.ContentType))
                        {
                            var imagesPath = "/Content/UserImages/";

                            var fileName = image.FileName;

                            var uploadPath = imagesPath + fileName;

                            var physicalPath = Server.MapPath(uploadPath);

                            image.SaveAs(physicalPath);

                            model.ImagePath = uploadPath;

                            sale.ImagePath = model.ImagePath;
                        }
                    }
                    else
                    {
                        sale.ImagePath = null;
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("List", new { id = model.Id });
            }

            return View(model);
        }

        private bool IsAuthorized(Sale sale)
        {
            var isAdmin = this.User.IsInRole("Admin");
            var isAuthor = sale.IsAuthor(this.User.Identity.GetUserId());

            return isAdmin || isAuthor;
        }
    }
}