namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TemporaryRole")]
    public partial class TemporaryRole
    {
        [Key]
        [Column(Order = 0)]
        public string TemporaryRoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public virtual AspNetRole AspNetRole { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
