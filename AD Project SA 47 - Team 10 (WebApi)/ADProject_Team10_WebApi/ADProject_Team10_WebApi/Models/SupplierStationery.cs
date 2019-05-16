namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierStationery")]
    public partial class SupplierStationery
    {
        public int SupplierStationeryId { get; set; }

        [Required]
        [StringLength(255)]
        public string SupplierCode { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public double UnitPrice { get; set; }

        public int SupplierRank { get; set; }

        public virtual Stationery Stationery { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
