namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RequisitionDetail
    {
        [Key]
        public int StatTransId { get; set; }

        public int RequisitionId { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int QuantityRequest { get; set; }

        public virtual Requisition Requisition { get; set; }

        public virtual Stationery Stationery { get; set; }
    }
}
