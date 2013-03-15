using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reprografia.Models.Account;
using Reprografia.Data;

namespace Reprografia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private ReprografiaContext db = new ReprografiaContext();

        //
        // GET: /Users/
        [Authorize(Roles = "Administrator")]
        public ViewResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5
        [Authorize(Roles = "Administrator")]
        public ViewResult Details(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /Users/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.UserName.ToLower();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Users/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}