using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentResult;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Areas.Admin.Service
{
    public class ProductService
    {
        private WebAccessoriesModel db = new WebAccessoriesModel();

        public Result<string> Create(ProductMV request)
        {
            if (request.DataImage != null)
            {
                var fileExtension = Path.GetExtension(request.DataImage.FileName).ToLower();

                // Kiểm tra kiểu
                var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!fileTypeSupported.Contains(fileExtension))
                {
                    return new ResultError<string>("Chỉ cho phép tập tin JPG, PNG, GIF!");
                }

                if (request.DataImage.ContentLength > 2 * 1024 * 1024)
                {
                    return new ResultError<string>("Chỉ cho phép tập tin từ 2MB trở xuống!");
                }
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileExtension)}";

                var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Storage/"), fileName);
                request.DataImage.SaveAs(filePath);

                // Cập nhật đường dẫn vào CSDL
                request.Image = $@"/Storage/{fileName}";

            }
            db.Products.Add(request.ConvertOrigin(request));
            db.SaveChanges();

            return new ResultSuccess<string>("OK");
        }

        public Result<string> Delete(Guid id)
        {
            var product = db.Products.Find(id);
            if (product == null) return new ResultError<string>("Mã hàng không tồn tại");

            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath($"{product.Image}"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return new ResultSuccess<string>();
        }

    }
}