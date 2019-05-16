namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        public int OrderDetailsId { get; set; }

        public int PoNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int Quantity { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public virtual Order Order { get; set; }

        public virtual Stationery Stationery { get; set; }
    }
}
