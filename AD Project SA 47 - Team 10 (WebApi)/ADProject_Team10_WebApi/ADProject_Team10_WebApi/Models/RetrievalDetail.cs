namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RetrievalDetail
    {
        [Key]
        public int RetrievalDetailsId { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int RetrievalId { get; set; }

        public int? QuantityRetrieved { get; set; }

        public int QuantityNeeded { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public virtual Retrieval Retrieval { get; set; }

        public virtual Stationery Stationery { get; set; }
    }
}
