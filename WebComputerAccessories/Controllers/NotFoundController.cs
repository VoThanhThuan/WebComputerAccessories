using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebComputerAccessories.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: NotFound
        public ActionResult index(string aspxerrorpath)
        {
            if (!string.IsNullOrWhiteSpace(aspxerrorpath))
                return RedirectToAction("index");
            return View();
        }
    }
}