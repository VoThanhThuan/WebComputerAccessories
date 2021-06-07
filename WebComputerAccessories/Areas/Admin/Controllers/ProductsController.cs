using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebComputerAccessories.Areas.Admin.Service;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Admin/Products
        [AllowAnonymous]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Stock,DateCreated,DataImage,Details,IdCategory")] ProductVM product)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            product.Id = Guid.NewGuid();
            product.DateCreated = DateTime.Now;
            var result = new ProductService().Create(product);

            if (result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name");
            return View(product);

        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name", product.IdCategory);
            return View(product.ConvertToVM());
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Stock,DateCreated,Image,DataImage,Details,IdCategory")] ProductVM product)
        {
            if (ModelState.IsValid)
            {
                if (product.DataImage != null)
                    product.Image = new ImageService().SaveImage(product.DataImage, "/Storage/").ResultObj;
                db.Entry(product.ConvertToOrigin()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name", product.IdCategory);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = new ProductService().Delete(id);
            if(!result.IsSuccessed)
                ModelState.AddModelError("", result.Message);
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
