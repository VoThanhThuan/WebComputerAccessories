using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebComputerAccessories.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["MaNguoiDung"] == null || (bool)Session["Quyen"] == true)
            {
                Session.Clear();
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "Login" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}