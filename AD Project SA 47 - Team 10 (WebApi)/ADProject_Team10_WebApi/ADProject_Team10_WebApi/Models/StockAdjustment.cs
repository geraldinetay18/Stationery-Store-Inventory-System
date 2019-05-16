namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockAdjustment
    {
        [Key]
        public int AdjustmentId { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int QuantityAdjusted { get; set; }

        [Required]
        [StringLength(255)]
        public string Reason { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(255)]
        public string ApproverRemarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateApproved { get; set; }

        public int ClerkEmployeeId { get; set; }

        public int? ApprovedByEmployeeId { get; set; }

        public int? VoucherNumber { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Stationery Stationery { get; set; }
    }
}
