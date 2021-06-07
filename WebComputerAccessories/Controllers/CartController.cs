using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Controllers
{
    public class CartController : BaseController
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        // GET: Cart
        public ActionResult Index()
        {
            if (Session["MaNguoiDung"] == null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ContentResult GetProductsJson()
        {
            var idUser = (Guid)Session["MaNguoiDung"];

            var userCart = db.Carts.FirstOrDefault(x => x.IdUser == idUser);
            if (userCart == null)
                return Content("");
            var details = (from dt in db.CartDetails
                           join p in db.Products on dt.IdProduct equals p.Id
                           where dt.IdCart == userCart.Id
                           select new {p, dt }).ToList();

            var prod = (details.Select(item => new DetailsCartVM()
            {
                Id = item.dt.Id,
                IdProduct = item.p.Id,
                Name = item.p.Name,
                Price = item.p.Price,
                Stock = item.p.Stock,
                Quantity = item.dt.Quantity,
                DateCreated = item.p.DateCreated,
                Image = item.p.Image,
                Details = item.p.Details,
                IdCategory = item.p.IdCategory
            })).ToList();

            var cart = new CartVM()
            {
                IdCart = userCart?.Id ?? Guid.Empty,
                Details = prod
            };

            Session["cart"] = cart;

            var json = JsonConvert.SerializeObject(cart);
            return Content(json, "application/json");
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
            if (Session["MaNguoiDung"] == null)
                return RedirectToAction("Index", "Home");//Đổi lại là trang login

            var idCart = Guid.Empty;
            var idUser = (Guid)Session["MaNguoiDung"];
            var cart = db.Carts.FirstOrDefault(x => x.IdUser == idUser);
            if (cart == null)
            {
                idCart = Guid.NewGuid();
                var c = new Cart()
                {
                    Id = idCart,
                    IdUser = idUser
                };
                db.Carts.Add(c);
                db.SaveChanges();
            }
            else
            {
                idCart = cart.Id;
            }

            var productInCart = db.CartDetails.FirstOrDefault(x => x.IdCart == idCart && x.IdProduct == id);
            if (productInCart == null)
            {
                var deltail = new CartDetail()
                {
                    Id = Guid.NewGuid(),
                    Quantity = quantity,
                    Price = product.Price * quantity,
                    IdCart = idCart,
                    IdProduct = product.Id
                };
                //db.Entry(product).State = EntityState.Modified;
                //product.Stock -= quantity;
                db.CartDetails.Add(deltail);
            }
            else
            {
                db.Entry(productInCart).State = EntityState.Modified;
                productInCart.Quantity += quantity;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("/UpdateIncrease/{id=id}")]
        public ActionResult UpdateIncrease(Guid id)
        {
            var products = (CartVM)Session["cart"];
            var product = products.Details.FirstOrDefault(x => x.Id == id);

            if (product.Quantity > product.Stock)
                return new HttpStatusCodeResult(400);

            product.Quantity++;

            Session["cart"] = products;
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("/UpdateDecrease/{id=id}")]
        public ActionResult UpdateDecrease(Guid id)
        {
            var products = (CartVM)Session["cart"];
            var product = products.Details.FirstOrDefault(x => x.Id == id);

            if (product.Quantity < 1)
                return new HttpStatusCodeResult(400);

            product.Quantity--;

            Session["cart"] = products;
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [Route("/Remove/{id=id}")]
        public ActionResult Remove(Guid id)
        {
            var products = (CartVM)Session["cart"];
            var product = products.Details.FirstOrDefault(x => x.Id == id);

            products.Details.Remove(product);

            var detail = db.CartDetails.FirstOrDefault(x => x.IdProduct == id);
            db.CartDetails.Remove(detail);

            db.SaveChanges();

            Session["cart"] = products;
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult Payment()
        {
            var cart = (CartVM)Session["cart"];

            var idUser = (Guid)Session["MaNguoiDung"];

            var user = db.AppUsers.Find(idUser);

            var idOrder = Guid.NewGuid();
            var order = new Order()
            {
                Id = idOrder,
                IdUser = user.Id,
                ShipName = $"{user.Lastname} {user.Firstname}",
                ShipAddress = "Đồ án demo không có địa chỉ",
                ShipPhoneNumber = user.PhoneNumber,
                Note = "Đây là độ án của Võ Thành Thuận & Nguyễn Ngọc Sơn"
            };
            db.Orders.Add(order);
            var totalMoney = 0d;
            foreach (var item in cart.Details)
            {
                var odt = new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    Quantity = item.Quantity,
                    Price = item.Price,
                    IdOrder = idOrder,
                    IdProduct = item.IdProduct
                };
                totalMoney += Convert.ToDouble(item.Quantity * item.Price);
                var product = db.Products.Find(item.IdProduct);
                db.Entry(product).State = EntityState.Modified;
                product.Stock -= item.Quantity;
                db.OrderDetails.Add(odt);
                db.SaveChanges();
            }

            Session["Cart"] = null;

            var details = db.CartDetails.Where(x => x.IdCart == cart.IdCart).ToList();
            db.CartDetails.RemoveRange(details);
            db.SaveChanges();

            var orderVM = new OrderVM()
            {
                ShipName = $"{user.Lastname} {user.Firstname}",
                ShipAddress = "Đồ án demo không có địa chỉ",
                ShipPhoneNumber = user.PhoneNumber,
                TotalMoney = totalMoney,
                Note = "Đây là độ án của Võ Thành Thuận & Nguyễn Ngọc Sơn"
            };
            return View(orderVM);
        }

        //private DetailsCartVM FindProduct(Guid id)
        //{
        //    var products = (List<DetailsCartVM>)Session["cart"];
        //    return products.FirstOrDefault(x => x.Id == id);
        //}

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