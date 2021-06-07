using System.Linq;
using System.Web;
using WebComputerAccessories.Models.ViewModel;

namespace WebComputerAccessories.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            CartDetails = new HashSet<CartDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public ProductVM ConvertToVM()
        {
            return new ProductVM()
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

        public List<ProductVM> ConvertListVM(List<Product> request)
        {
            return request.Select(item => new ProductVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Stock = item.Stock,
                    DateCreated = item.DateCreated,
                    Image = item.Image,
                    Details = item.Details,
                    IdCategory = item.IdCategory
                })
                .ToList();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        [Display(Name = "Tên Sản Phẩm")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá không được bỏ trống!")]
        [Display(Name = "Giá")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Số lượng không được bỏ trống!")]
        [Display(Name = "Số Lượng Tồn")]
        public int? Stock { get; set; }

        [Display(Name = "Ngày Tạo")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Hình Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Chi Tiết")]
        public string Details { get; set; }

        public int? IdCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
