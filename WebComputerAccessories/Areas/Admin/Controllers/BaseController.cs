using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebComputerAccessories.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (Session["MaNguoiDung"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "Login" }));
            }
            //else
            //{
            //    if (((bool)Session["Quyen"]) == false)
            //    {
            //        if (controllerName != "LoginController")
            //            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "Login"}));
            //    }
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}