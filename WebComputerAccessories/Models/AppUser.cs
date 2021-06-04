namespace WebComputerAccessories.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppUser()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Tài khoản")]
        [StringLength(255)]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Tên")]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Display(Name = "Họ")]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Display(Name = "SĐT")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ngày Sinh")]
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [Display(Name = "Ảnh Đại Diện")]
        public string Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
