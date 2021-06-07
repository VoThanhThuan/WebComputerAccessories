namespace WebComputerAccessories.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartDetail")]
    public partial class CartDetail
    {
        public Guid Id { get; set; }

        [Display(Name = "Số Lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Giá")]
        public double? Price { get; set; }

        public Guid? IdCart { get; set; }

        public Guid? IdProduct { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}
