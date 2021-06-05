using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class ProductVM
    {
        public Product ConvertToOrigin()
        {
            return new Product()
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Stock = this.Stock,
                DateCreated = this.DateCreated,
                Image = this.Image,
                Details = this.Details,
                IdCategory = this.IdCategory
            };

        }
        public string Json { get; set; }


        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        [Display(Name = "Tên Sản Phẩm")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá không được bỏ trống!")]
        [Display(Name = "Giá")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Số lượng không được bỏ trống!")]
        [Range(1, 1000, ErrorMessage = "Số lượng từ 1 đến 1000 thôi!")]
        [Display(Name = "Số Lượng Tồn")]
        public int? Stock { get; set; }

        [Display(Name = "Ngày Tạo")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Hình Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Chi Tiết")]
        public string Details { get; set; }

        [Display(Name = "Loại Hàng")]
        public int? IdCategory { get; set; }

        [Display(Name = "Hình Ảnh")]
        public HttpPostedFileBase DataImage { get; set; }

        public virtual Category Category { get; set; }

    }

}