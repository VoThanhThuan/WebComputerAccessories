namespace WebComputerAccessories.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public Guid Id { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }
        [Display(Name = "Giá")]
        public double? Price { get; set; }

        public Guid? IdOrder { get; set; }

        public Guid? IdProduct { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
