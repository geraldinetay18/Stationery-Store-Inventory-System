/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10.Models
{
    public class StockMgmtMaintain
    {
        private int stockManagmentId;
        private String date;
        private String itemCode;
        private int storeClerkId;
        private String source;
        private int sourceId;
        private int qtyAdjusted;
        private int balance;
        private String reason;

        private int deptId;
        private String deptName;
        private int suplId;
        private String suplName;
        private String supl_dept;

        public int StockManagmentId { get => stockManagmentId; set => stockManagmentId = value; }
        public string Date { get => date; set => date = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public int StoreClerkId { get => storeClerkId; set => storeClerkId = value; }
        public string Source { get => source; set => source = value; }
        public int SourceId { get => sourceId; set => sourceId = value; }
        public int QtyAdjusted { get => qtyAdjusted; set => qtyAdjusted = value; }
        public int Balance { get => balance; set => balance = value; }
        public string Reason { get => reason; set => reason = value; }
        public int DeptId { get => deptId; set => deptId = value; }
        public string DeptName { get => deptName; set => deptName = value; }
        public int SuplId { get => suplId; set => suplId = value; }
        public string SuplName { get => suplName; set => suplName = value; }
        public string Supl_dept { get => supl_dept; set => supl_dept = value; }
    }
}