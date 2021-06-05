using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebComputerAccessories.Models;
namespace WebComputerAccessories.Controllers
{
    public class AdditionnalCartController : Controller
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();
        // GET: AdditionnalCart
        public ActionResult Index()
        {
            return View();
        }

    }
}