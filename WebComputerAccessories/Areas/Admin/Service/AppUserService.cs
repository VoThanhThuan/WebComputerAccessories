using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FluentResult;
using WebComputerAccessories.Models;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Areas.Admin.Service
{
    public class AppUserService
    {
        private readonly WebAccessoriesModel db = new WebAccessoriesModel();

        public Result<string> Create(AppUserVM request)
        {
            if (request.AvatarData != null)
            {
                var fileExtension = Path.GetExtension(request.AvatarData.FileName).ToLower();

                // Kiểm tra kiểu
                var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!fileTypeSupported.Contains(fileExtension))
                {
                    return new ResultError<string>("Chỉ cho phép tập tin JPG, PNG, GIF!");
                }

                if (request.AvatarData.ContentLength > 2 * 1024 * 1024)
                {
                    return new ResultError<string>("Chỉ cho phép tập tin từ 2MB trở xuống!");
                }
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileExtension)}";

                var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Storage/avatar/"), fileName);
                request.AvatarData.SaveAs(filePath);

                // Cập nhật đường dẫn vào CSDL
                request.Avatar = $@"/Storage/avatar/{fileName}";

            }

            request.PasswordHash = new Encrypt().EncryptSHA256(request.PasswordHash);
            db.AppUsers.Add(request.ConvertOrigin());
            db.SaveChanges();
            return new ResultSuccess<string>("OK");
        }

        public Result<string> Delete(Guid id)
        {
            var user = db.AppUsers.Find(id);
            if (user == null) return new ResultError<string>("Mã hàng không tồn tại");
            new CartService().Delete(id);
            new ImageService().DeleteImage(user.Avatar);

            db.AppUsers.Remove(user);
            db.SaveChanges();
            return new ResultSuccess<string>();
        }


    }
}