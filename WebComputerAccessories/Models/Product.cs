using System.Web;

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

        public Guid Id { get; set; }

        [Display(Name = "Tên Sản Phẩm")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Giá")]
        public double? Price { get; set; }

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
