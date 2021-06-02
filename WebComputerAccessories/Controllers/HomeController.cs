using System;
using System.Collections.Generic;
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
            var proct = db.Products.Select(x => x).ToList();
            var json = JsonConvert.SerializeObject(new ProductsMV().ConvertMV(proct));
            return View(new DataJson() { Data = json });
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
    }
    
}