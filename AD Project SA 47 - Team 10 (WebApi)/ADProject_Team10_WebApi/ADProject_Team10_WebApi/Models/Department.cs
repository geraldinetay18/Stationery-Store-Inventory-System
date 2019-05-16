namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Disbursements = new HashSet<Disbursement>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        [StringLength(255)]
        public string DeptId { get; set; }

        public int StoreClerkId { get; set; }

        public int RepresentativeId { get; set; }

        public int HeadId { get; set; }

        [Required]
        [StringLength(255)]
        public string LocationId { get; set; }

        [Required]
        [StringLength(255)]
        public string DeptName { get; set; }

        [Column("Contact Name")]
        [Required]
        [StringLength(255)]
        public string Contact_Name { get; set; }

        [Required]
        [StringLength(255)]
        public string TelephoneNo { get; set; }

        [Required]
        [StringLength(255)]
        public string Fax { get; set; }

        public virtual CollectionPoint CollectionPoint { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disbursement> Disbursements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
