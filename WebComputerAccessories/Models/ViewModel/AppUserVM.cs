using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class AppUserVM
    {
        public AppUser ConvertOrigin()
        {
            return new AppUser()
            {
                Id = this.Id,
                Username = this.Username,
                PasswordHash = this.PasswordHash,
                Email = this.Email,
                Firstname = this.Firstname,
                Lastname = this.Lastname,
                PhoneNumber = this.PhoneNumber,
                Dob = this.Dob,
                Avatar = this.Avatar
            };

        }
        public Guid Id { get; set; }

        [Display(Name = "Tài khoản")]
        [StringLength(255)]
        [Required(ErrorMessage = "Tài khoản không được bỏ trống!")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(255)]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string PasswordHash { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        public string Email { get; set; }

        [Display(Name = "Tên")]
        [StringLength(50)]
        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        public string Firstname { get; set; }

        [Display(Name = "Họ")]
        [StringLength(50)]
        [Required(ErrorMessage = "Họ không được bỏ trống!")]
        public string Lastname { get; set; }

        [Display(Name = "SĐT")]
        [StringLength(12)]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ngày Sinh")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

        [Display(Name = "Ảnh Đại Diện")]
        public string Avatar { get; set; }

        [Display(Name = "Ảnh Đại Diện")]
        public HttpPostedFileBase AvatarData { get; set; }

    }
}