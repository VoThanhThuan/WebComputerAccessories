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

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        public Guid? IdCart { get; set; }

        public Guid? IdProduct { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}
