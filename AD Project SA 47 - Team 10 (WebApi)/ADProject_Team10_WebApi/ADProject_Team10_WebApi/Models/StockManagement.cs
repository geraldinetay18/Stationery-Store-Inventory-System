namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockManagement")]
    public partial class StockManagement
    {
        public int StockManagementId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int StoreClerkId { get; set; }

        [Required]
        [StringLength(255)]
        public string Source { get; set; }

        public int SourceId { get; set; }

        public int QtyAdjusted { get; set; }

        public int Balance { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Stationery Stationery { get; set; }
    }
}
