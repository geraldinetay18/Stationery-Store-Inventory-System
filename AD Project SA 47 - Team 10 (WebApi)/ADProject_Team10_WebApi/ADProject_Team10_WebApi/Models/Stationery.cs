namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stationery")]
    public partial class Stationery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stationery()
        {
            DisbursementDetails = new HashSet<DisbursementDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            RequisitionDetails = new HashSet<RequisitionDetail>();
            RetrievalDetails = new HashSet<RetrievalDetail>();
            StockAdjustments = new HashSet<StockAdjustment>();
            StockManagements = new HashSet<StockManagement>();
            SupplierStationeries = new HashSet<SupplierStationery>();
        }

        [Key]
        [StringLength(255)]
        public string ItemCode { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public int QuantityInStock { get; set; }

        public int QuantityReorder { get; set; }

        public int ReorderLevel { get; set; }

        [Required]
        [StringLength(255)]
        public string UnitOfMeasure { get; set; }

        public int Bin { get; set; }

        [StringLength(255)]
        public string AdjustmentRemark { get; set; }

        public int OverRequestFrequency { get; set; }

        public int RecommandQuantity { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DisbursementDetail> DisbursementDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequisitionDetail> RequisitionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RetrievalDetail> RetrievalDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockAdjustment> StockAdjustments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockManagement> StockManagements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierStationery> SupplierStationeries { get; set; }
    }
}
