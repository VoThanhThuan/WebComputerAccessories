using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebComputerAccessories.Areas.Admin.Service;
using WebComputerAccessories.Models;

namespace WebComputerAccessories.Areas.Admin.Controllers
{
    public class CartsController : Controller
    {
        private readonly WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Admin/Carts
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.AppUser);
            return View(carts.ToList());
        }

        // GET: Admin/Carts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Admin/Carts/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.AppUsers, "Id", "Username");
            return View();
        }

        // POST: Admin/Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUser")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.Id = Guid.NewGuid();
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.AppUsers, "Id", "Username", cart.IdUser);
            return View(cart);
        }

        // GET: Admin/Carts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.AppUsers, "Id", "Username", cart.IdUser);
            return View(cart);
        }

        // POST: Admin/Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUser")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.AppUsers, "Id", "Username", cart.IdUser);
            return View(cart);
        }

        // GET: Admin/Carts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Admin/Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            new CartService().Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
