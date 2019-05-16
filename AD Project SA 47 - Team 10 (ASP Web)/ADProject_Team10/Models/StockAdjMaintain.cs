/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10.Models
{
    public class StockAdjMaintain
    {
        private int adjustmentId;
        private String itemCode;
        private int quantityAdjusted;
        private String reason;
        private String status;
        private String approverRemarks;
        private String dateCreated;
        private String dateApproved;
        private int clerkEmployeeId;
        private int approvedByEmployeeId;
        private int voucherNumber;
        private String departmentName;
        private String supplierName;

        public int AdjustmentId { get => adjustmentId; set => adjustmentId = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public int QuantityAdjusted { get => quantityAdjusted; set => quantityAdjusted = value; }
        public string Reason { get => reason; set => reason = value; }
        public string Status { get => status; set => status = value; }
        public string ApproverRemarks { get => approverRemarks; set => approverRemarks = value; }
        public String DateCreated { get => dateCreated; set => dateCreated = value; }
        public String DateApproved { get => dateApproved; set => dateApproved = value; }
        public int ClerkEmployeeId { get => clerkEmployeeId; set => clerkEmployeeId = value; }
        public int ApprovedByEmployeeId { get => approvedByEmployeeId; set => approvedByEmployeeId = value; }
        public int VoucherNumber { get => voucherNumber; set => voucherNumber = value; }
        public string DepartmentName { get => departmentName; set => departmentName = value; }
        public string SupplierName { get => supplierName; set => supplierName = value; }
    }
}