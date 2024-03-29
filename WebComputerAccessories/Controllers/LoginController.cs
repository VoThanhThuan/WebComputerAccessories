﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentResult;
using WebComputerAccessories.Areas.Admin.Service;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Controllers
{
    public class LoginController : Controller
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            var passwordHash = new Encrypt().EncryptSHA256(request.Password);
            var account =
                db.AppUsers.FirstOrDefault(x => x.Username == request.Username && x.PasswordHash == passwordHash);

            if (account == null)
            {
                ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                return View(request);
            }


            // Đăng ký SESSION
            Session["MaNguoiDung"] = account.Id;
            Session["HoVaTen"] = $"{account.Lastname} {account.Firstname}";
            Session["Quyen"] = account.Role;

            // Quay về trang chủ
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,PasswordHash,Email,Firstname,Lastname,PhoneNumber,Dob,AvatarData")] AppUserVM appUser)
        {
            if (!ModelState.IsValid) RedirectToAction("Index");
            appUser.Id = Guid.NewGuid();

            appUser.Role = false;
            var result = new AppUserService().Create(appUser);
            if (result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            return RedirectToAction("Index");

        }

        public ActionResult SignOut()
        {
            // Hủy ký SESSION
            Session.Clear();
            return RedirectToAction("Index", "Home");
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