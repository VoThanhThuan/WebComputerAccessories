using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentResult;
using WebComputerAccessories.Models;

namespace WebComputerAccessories.Areas.Admin.Service
{
    public class CartService
    {
        private readonly WebAccessoriesModel db = new WebAccessoriesModel();

        public Result<string> Delete(Guid id, bool idUser = false)
        {
            var cart = idUser != false ? db.Carts.FirstOrDefault(x => x.IdUser == id) : db.Carts.Find(id);
            if (cart == null)
                return new ResultError<string>("Cart không tồn tại");

            var details = db.CartDetails.Where(x => x.IdCart == cart.Id).ToList();
            if (details.Count > 0)
            {
                db.CartDetails.RemoveRange(details);
                db.SaveChanges();
            }

            db.Carts.Remove(cart);
            db.SaveChanges();
            return new ResultError<string>("OK");
        }
    }
}