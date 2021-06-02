using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebComputerAccessories.Models;

namespace WebComputerAccessories.Areas.Admin.Controllers
{
    public class CartDetailsController : Controller
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Admin/CartDetails
        public ActionResult Index()
        {
            var cartDetails = db.CartDetails.Include(c => c.Cart).Include(c => c.Product);
            return View(cartDetails.ToList());
        }

        // GET: Admin/CartDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // GET: Admin/CartDetails/Create
        public ActionResult Create()
        {
            ViewBag.IdCart = new SelectList(db.Carts, "Id", "Id");
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: Admin/CartDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,Price,IdCart,IdProduct")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                cartDetail.Id = Guid.NewGuid();
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCart = new SelectList(db.Carts, "Id", "Id", cartDetail.IdCart);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", cartDetail.IdProduct);
            return View(cartDetail);
        }

        // GET: Admin/CartDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCart = new SelectList(db.Carts, "Id", "Id", cartDetail.IdCart);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", cartDetail.IdProduct);
            return View(cartDetail);
        }

        // POST: Admin/CartDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,Price,IdCart,IdProduct")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCart = new SelectList(db.Carts, "Id", "Id", cartDetail.IdCart);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", cartDetail.IdProduct);
            return View(cartDetail);
        }

        // GET: Admin/CartDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // POST: Admin/CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            db.CartDetails.Remove(cartDetail);
            db.SaveChanges();
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
