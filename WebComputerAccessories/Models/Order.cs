namespace WebComputerAccessories.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Tên Đơn Hàng")]
        [StringLength(255)]
        public string ShipName { get; set; }

        [Display(Name = "Địa Chỉ Giao")]
        public string ShipAddress { get; set; }

        [Display(Name = "Số Điện Thoại Giao Hàng")]
        [StringLength(12)]
        public string ShipPhoneNumber { get; set; }

        [Display(Name = "Ghi Chú")]
        [StringLength(255)]
        public string Note { get; set; }

        public Guid? IdUser { get; set; }

        public virtual AppUser AppUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
