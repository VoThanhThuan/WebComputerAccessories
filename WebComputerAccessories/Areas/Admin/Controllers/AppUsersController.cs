using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FluentResult;
using WebComputerAccessories.Areas.Admin.Service;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;
namespace WebComputerAccessories.Areas.Admin.Controllers
{
    public class AppUsersController : BaseController
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Admin/AppUsers
        public ActionResult Index()
        {
            return View(db.AppUsers.ToList());
        }

        // GET: Admin/AppUsers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: Admin/AppUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,PasswordHash,Email,Firstname,Lastname,PhoneNumber,Dob,AvatarData,Role")] AppUserVM appUser)
        {
            if (!ModelState.IsValid) return View(appUser);
            appUser.Id = Guid.NewGuid();

            var result = new AppUserService().Create(appUser);
            if (result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            return View(appUser);

        }

        // GET: Admin/AppUsers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser.ConvertToVM());
        }

        // POST: Admin/AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,PasswordHash,Email,Firstname,Lastname,PhoneNumber,Dob,Avatar,AvatarData,Role")] AppUserVM appUser)
        {
            ModelState["PasswordHash"].Errors.Clear();
            if (!ModelState.IsValid) return View(appUser);
            if (appUser.AvatarData != null)
                appUser.Avatar = new ImageService().SaveImage(appUser.AvatarData, "/Storage/avatar/").ResultObj;
            if(!string.IsNullOrEmpty(appUser.PasswordHash))
                appUser.PasswordHash = new Encrypt().EncryptSHA256(appUser.PasswordHash);

            var user = appUser.ConvertOrigin();
            db.Entry(user).State = EntityState.Modified;
            if (string.IsNullOrEmpty(appUser.PasswordHash))
                db.Entry(user).Property(x => x.PasswordHash).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/AppUsers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: Admin/AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result1 = new CartService().Delete(id, true);
            if (!result1.IsSuccessed)
                ModelState.AddModelError("", result1.Message);

            var result2 = new OrderService().Delete(id, true);
            if (!result2.IsSuccessed)
                ModelState.AddModelError("", result2.Message);

            var result = new AppUserService().Delete(id);
            if (!result.IsSuccessed)
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
