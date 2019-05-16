namespace ADProject_Team10_WebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SSAEntities : DbContext
    {
        public SSAEntities()
            : base("name=SSAEntities")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CollectionPoint> CollectionPoints { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Disbursement> Disbursements { get; set; }
        public virtual DbSet<DisbursementDetail> DisbursementDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Requisition> Requisitions { get; set; }
        public virtual DbSet<RequisitionDetail> RequisitionDetails { get; set; }
        public virtual DbSet<Retrieval> Retrievals { get; set; }
        public virtual DbSet<RetrievalDetail> RetrievalDetails { get; set; }
        public virtual DbSet<Stationery> Stationeries { get; set; }
        public virtual DbSet<StockAdjustment> StockAdjustments { get; set; }
        public virtual DbSet<StockManagement> StockManagements { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierStationery> SupplierStationeries { get; set; }
        public virtual DbSet<TemporaryRole> TemporaryRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.TemporaryRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.TemporaryRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Stationeries)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CollectionPoint>()
                .HasMany(e => e.Departments)
                .WithRequired(e => e.CollectionPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Disbursements)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DeptId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Disbursement>()
                .HasMany(e => e.DisbursementDetails)
                .WithRequired(e => e.Disbursement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CollectionPoints)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Departments)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.StoreClerkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Departments1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Departments2)
                .WithRequired(e => e.Employee2)
                .HasForeignKey(e => e.RepresentativeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Disbursements)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.StoreClerkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Disbursements1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.RepresentativeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.OrderEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Requisitions)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Requisitions1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ApprovedByEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Retrievals)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StockAdjustments)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ApprovedByEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StockAdjustments1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.ClerkEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StockManagements)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.StoreClerkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TemporaryRoles)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requisition>()
                .HasMany(e => e.RequisitionDetails)
                .WithRequired(e => e.Requisition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Retrieval>()
                .HasMany(e => e.RetrievalDetails)
                .WithRequired(e => e.Retrieval)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.DisbursementDetails)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.RequisitionDetails)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.RetrievalDetails)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.StockAdjustments)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.StockManagements)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stationery>()
                .HasMany(e => e.SupplierStationeries)
                .WithRequired(e => e.Stationery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.SupplierStationeries)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);
        }
    }
}
