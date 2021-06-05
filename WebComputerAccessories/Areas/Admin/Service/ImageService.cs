using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FluentResult;

namespace WebComputerAccessories.Areas.Admin.Service
{
    public class ImageService
    {
        public Result<string> SaveImage(HttpPostedFileBase dataImage, string folderPath)
        {
            if (dataImage == null) return new ResultError<string>();
            var fileExtension = Path.GetExtension(dataImage.FileName).ToLower();

            // Kiểm tra kiểu
            var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            if (!fileTypeSupported.Contains(fileExtension))
                return new ResultError<string>("Chỉ cho phép tập tin JPG, PNG, GIF, WEBP!");

            if (dataImage.ContentLength > 2 * 1024 * 1024)
                return new ResultError<string>("Chỉ cho phép tập tin từ 2MB trở xuống!");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileExtension)}";

            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath($"{folderPath}"), fileName);
            dataImage.SaveAs(filePath);
            // trả lại đường dẫn vào CSDL
            return new ResultSuccess<string>($@"{folderPath}{fileName}", "Lưu ảnh thành công");

        }

        public void DeleteImage(string imgaePath)
        {
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath($"imgaePath"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}