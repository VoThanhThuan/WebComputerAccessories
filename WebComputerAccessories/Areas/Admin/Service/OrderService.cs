using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentResult;
using WebComputerAccessories.Models;

namespace WebComputerAccessories.Areas.Admin.Service
{
    public class OrderService
    {
        private readonly WebAccessoriesModel db = new WebAccessoriesModel();

        public Result<string> Delete(Guid id, bool? idUser = null)
        {
            var order = idUser != false ? db.Orders.FirstOrDefault(x => x.IdUser == id) : db.Orders.Find(id);

            if (order == null)
                return new ResultError<string>("Đơn hàng không tồn tại");

            var details = db.OrderDetails.Where(x => x.IdOrder == order.Id).ToList();
            if (details.Count > 0)
            {
                db.OrderDetails.RemoveRange(details);
                db.SaveChanges();
            }

            db.Orders.Remove(order);
            db.SaveChanges();
            return new ResultError<string>("OK");
        }

    }
}