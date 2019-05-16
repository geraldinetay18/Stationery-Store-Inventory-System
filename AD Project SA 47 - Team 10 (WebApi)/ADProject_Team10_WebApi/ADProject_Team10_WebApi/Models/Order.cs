namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int PoNumber { get; set; }

        public int DoNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string SupplierCode { get; set; }

        public int OrderEmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOrdered { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateDelivery { get; set; }

        [Required]
        [StringLength(255)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateSupply { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
