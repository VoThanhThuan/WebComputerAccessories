﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Controllers
{
    public class HomeController : Controller
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();
        public ActionResult Index(){
            db.Products.Load();
            var proct = db.Products.Select(x => x).ToList();
            var json = JsonConvert.SerializeObject(new Product().ConvertListVM(proct));
            return View(new DataJson() { Data = json });
        }
        [HttpPost]
        [Route("/GetProductsJson/{skip=skip}/{take=take}")]
        public ContentResult GetProductsJson(int skip, int take)
        {
            var proct = db.Products.OrderByDescending(x => x.DateCreated).Skip(skip).Take(take).ToList();
            var json = JsonConvert.SerializeObject(new Product().ConvertListVM(proct));
            return Content(json, "application/json");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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