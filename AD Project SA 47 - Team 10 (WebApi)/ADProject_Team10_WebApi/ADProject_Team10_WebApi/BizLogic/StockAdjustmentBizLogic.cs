/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;

namespace ADProject_Team10_WebApi.BizLogic
{
    using Models;
    using Services;
    public class StockAdjustmentBizLogic
    {
        IStockAdjustmentService saService = new StockAdjustmentService();
        IEmployeeService eService = new EmployeeService();
        IStationeryService stService = new StationeryService();
        ICategoryService cService = new CategoryService();

        public List<StockAdjustment> FindAllBelow250Pending()
        {
            return saService.FindAllBelow250Pending();
        }

        public List<StockAdjustment> FindAllAbove250Pending()
        {
            return saService.FindAllAbove250Pending();
        }

        public List<StockAdjustment> FindAllVouchers()
        {
            return saService.FindAllVouchers();
        }
        public List<StockAdjustment> FindAllAdjustmentsOfVoucher(int voucherNumber)
        {
            return saService.FindAllAdjustmentsOfVoucher(voucherNumber);
        }

        public string FindEmpNameById(int id)
        {
            return eService.SearchEmployeeByEmpId(id).EmployeeName;
        }

        public double FindTotalCostOfVoucher(int voucherNumber)
        {
            return saService.FindTotalCostOfVoucher(voucherNumber);
        }

        public string FindDescription(string itemCode)
        {
            return stService.FindStationeryById(itemCode).Description;
        }

        public double FindAdjustmentCost(string itemCode, int quantity)
        {
            Stationery s = stService.FindStationeryById(itemCode);
            double cost = 0;

            // First cheapest quotation of stationery
            foreach (SupplierStationery supplier in s.SupplierStationeries)
            {
                if (supplier.SupplierRank == 1)
                {
                    cost = supplier.UnitPrice * Math.Abs(quantity);
                    break;
                }
            }
            return cost;
        }

        public int GenerateVoucherNumber()
        {
            // By business rule, Voucher numbers are incremented by 1
            return (saService.FindLastVoucherNumber() + 1);
        }

        public void ApproveAdjustmentList(List<int> listAdjustmentId, string remarks, int approvedByEmployeeId)
        {
            // Generate voucherNumber
            int newVoucherNumber = saService.FindLastVoucherNumber() + 1;

            foreach (int id in listAdjustmentId)
            {
                // 1. ADJUSTMENT
                StockAdjustment s = saService.FindAdjustmentById(id); // Associated adjustment object

                // Check if negative adjustments exceeded stock quantity
                // Modify adjusted quantity and add remarks for user
                int currentQty = FindCurrentQuantityByItemCode(s.ItemCode);
                if ((currentQty + s.QuantityAdjusted) < 0)
                {
                    s.QuantityAdjusted = -currentQty;
                    remarks += " SYSTEM GENERATED-As user input negative adjustment exceeded " +
                        "current quantity, adjusted quantity would be modified to reduce current quantity to zero.";
                }

                s.ApproverRemarks = remarks;
                s.DateApproved = DateTime.Today;
                s.ApprovedByEmployeeId = approvedByEmployeeId;
                s.VoucherNumber = newVoucherNumber;

                // 2. STATIONERY
                Stationery sta = stService.FindStationeryById(s.ItemCode);
                sta.QuantityInStock += s.QuantityAdjusted;

                // 3. BOTH
                saService.ApproveAdjustment(s);
                stService.UpdateStationery(sta);
            }
        }

        public void RejectAdjustmentList(List<int> listAdjustmentId, string remarks, int approvedByEmployeeId)
        {
            foreach (int id in listAdjustmentId)
            {
                // Get associated adjustment object
                StockAdjustment s = saService.FindAdjustmentById(id);
                s.ApproverRemarks = remarks;
                s.DateApproved = DateTime.Today;
                s.ApprovedByEmployeeId = approvedByEmployeeId;
                saService.RejectAdjustment(s);
            }
        }

        public void ReportAdjustmentList(List<int> listAdjustmentId)
        {
            foreach (int id in listAdjustmentId)
                saService.ReportAdjustment(id);
        }

        public string FindSupervisorName()
        {
            return eService.SearchStoreSupervisor().EmployeeName;
        }

        public List<StockAdjustment> FindAllReported()
        {
            return saService.FindAllReported();
        }

        public List<StockAdjustment> FindAllAdjustmentsOfClerkWithFilter(int clerkId, string status)
        {
            return saService.FindAdjustmentsOfClerkWithFilter(clerkId, status);
        }

        public List<Category> FindAllCategories()
        {
            return cService.FindAllCategories();
        }

        public List<Stationery> FindAllStationeryByCategory(int categoryId)
        {
            return stService.FindAllStationeryByCategory(categoryId);
        }

        public int FindCurrentQuantityByItemCode(string code)
        {
            return stService.FindStationeryById(code).QuantityInStock;
        }

        public int AddAdjustment(StockAdjustment adjustment, string status)
        {
            if (status == "Pending" || status == "In Progress")
                adjustment.Status = status;
            else if (status == "In Progress")
                adjustment.Status = status;

            adjustment.DateCreated = DateTime.Now;
            return saService.AddStockAdjustment(adjustment);
        }

        public int UpdateAdjustment(StockAdjustment adjustment, string status)
        {
            if (status == "Pending")
                adjustment.Status = status;
            else if (status == "In Progress")
                adjustment.Status = status;

            return saService.UpdateAdjustment(adjustment);
        }

        public StockAdjustment FindAdjustmentById(int id)
        {
            return saService.FindAdjustmentById(id);
        }
    }
}