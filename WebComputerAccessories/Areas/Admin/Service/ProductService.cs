using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FluentResult;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Areas.Admin.Service
{
    public class ProductService
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        public Result<string> Create(ProductVM request)
        {
            var result = new ImageService().SaveImage(request.DataImage, "/Storage/");
            if (!result.IsSuccessed) return new ResultError<string>(result.Message);
            request.Image = result.ResultObj;
            db.Products.Add(request.ConvertToOrigin());
            db.SaveChanges();

            return new ResultSuccess<string>("OK");
        }

        public Result<string> Delete(Guid id)
        {
            var product = db.Products.Find(id);
            if (product == null) return new ResultError<string>("Mã hàng không tồn tại");

            new ImageService().DeleteImage(product.Image);

            db.Products.Remove(product);
            db.SaveChanges();
            return new ResultSuccess<string>();
        }

    }
}