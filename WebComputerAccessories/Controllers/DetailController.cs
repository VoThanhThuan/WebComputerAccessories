using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using WebComputerAccessories.Models;
namespace WebComputerAccessories.Controllers
{
    public class DetailController : Controller
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: AdditionnalCart
        [Route("{id}")]
        public ActionResult Index(Guid? id)
        {
            if(id == null)
                return RedirectToAction("Index", "Home");
            var product = db.Products.Find(id);
            return View(product);
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