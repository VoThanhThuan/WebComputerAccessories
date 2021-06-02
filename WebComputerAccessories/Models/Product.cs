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

        [StringLength(255)]
        public string Name { get; set; }

        public double? Price { get; set; }

        public int? Stock { get; set; }

        public DateTime? DateCreated { get; set; }

        public string Image { get; set; }

        public string Details { get; set; }

        public int? IdCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
