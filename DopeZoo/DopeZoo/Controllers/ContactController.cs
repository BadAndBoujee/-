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
    public class ContactController : Controller
    {
        public ActionResult List()
        {
            using (var db = new ContactDbContext())
            {
                var contacts = db.Contacts
                    .Include(a => a.Name)
                    .ToList();

                return View(contacts);
            }
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ContactDbContext())
                {
                    var authorId = this.User.Identity.GetUserId();

                    model.AuthorId = authorId;

                    db.Contacts.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}