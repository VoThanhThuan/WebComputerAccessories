using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebComputerAccessories.Models;

namespace WebComputerAccessories.Controllers
{
    public class CartController : Controller
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // cart?id=<id>&quantity=
        [HttpPost]
        public ActionResult Add(Guid id, int quantity)
        {
           var product = db.Products.Find(id);
           if (product == null)
               return RedirectToAction("Index", "Home");
           if (product.Stock < 1)
               return RedirectToAction("Index", "Cart", id);
           if(Session["MaNguoiDung"] == null)
               return RedirectToAction("Index", "Home");

           var idCart = Guid.Empty;
           var cart = db.Carts.FirstOrDefault(x => x.IdUser == (Guid) Session["MaNguoiDung"]);
           if (cart == null)
           {
               idCart = new Guid();
               var c = new Cart()
               {
                   Id = idCart,
                   IdUser = (Guid)Session["MaNguoiDung"]
               };
               db.Carts.Add(c);
               db.SaveChanges();
           }
           else
           {
               idCart = cart.Id;
           }

           var deltail = new CartDetail()
           {
               Id = Guid.NewGuid(),
               Quantity = quantity,
               Price = product.Price * quantity,
               IdCart = idCart,
               IdProduct = product.Id
           };

           db.CartDetails.Add(deltail);

           return null;
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