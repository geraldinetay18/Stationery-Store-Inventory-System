namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DisbursementDetail
    {
        [Key]
        public int DisbursementDetailsId { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int DisbursementId { get; set; }

        public int QuantityRequested { get; set; }

        public int QuantityReceived { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public virtual Disbursement Disbursement { get; set; }

        public virtual Stationery Stationery { get; set; }
    }
}
