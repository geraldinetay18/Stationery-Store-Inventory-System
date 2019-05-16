/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        public int AddStockAdjustment(StockAdjustment adjustment)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.StockAdjustments.Add(adjustment);
                return context.SaveChanges(); // returns 1
            }
            catch (Exception e)
            {
                return 0; // 0 rows added
            }
        }

        public int ApproveAdjustment(StockAdjustment adjustment)
        {
            SSAEntities context = new SSAEntities();

            try
            {
                StockAdjustment s = context.StockAdjustments
                                                                   .Where(x => x.AdjustmentId == adjustment.AdjustmentId).FirstOrDefault();
                s.Status = "Approved";
                s.QuantityAdjusted = adjustment.QuantityAdjusted;
                s.ApproverRemarks = adjustment.ApproverRemarks;
                s.DateApproved = adjustment.DateApproved;
                s.ApprovedByEmployeeId = adjustment.ApprovedByEmployeeId;
                s.VoucherNumber = adjustment.VoucherNumber;

                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 rows updated
            }
        }

        public int RejectAdjustment(StockAdjustment adjustment)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                StockAdjustment s = context.StockAdjustments
                                                                   .Where(x => x.AdjustmentId == adjustment.AdjustmentId).FirstOrDefault();
                s.Status = "Rejected";
                s.ApproverRemarks = adjustment.ApproverRemarks;
                s.DateApproved = adjustment.DateApproved;
                s.ApprovedByEmployeeId = adjustment.ApprovedByEmployeeId;

                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 rows updated
            }
        }

        public int DeleteAdjustmentById(int adjustmentId)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                StockAdjustment s = context.StockAdjustments
                                                                   .Where(x => x.AdjustmentId == adjustmentId).FirstOrDefault();
                context.StockAdjustments.Remove(s);
                return context.SaveChanges(); // 1 row deleted
            }
            catch (Exception e)
            {
                return 0; // 0 rows deleted
            }
        }

        public StockAdjustment FindAdjustmentById(int adjustmentId)
        {
            SSAEntities context = new SSAEntities();
            StockAdjustment adjustment = context.StockAdjustments
                                                                    .Where(x => x.AdjustmentId == adjustmentId).FirstOrDefault();
            return adjustment;
        }

        public List<StockAdjustment> FindAllAbove250Pending()
        {

            SSAEntities context = new SSAEntities();
            List<StockAdjustment> listAbove = new List<StockAdjustment>(); // For appending adjustments >$250 

            // Loop through all Pending adjustments
            foreach (StockAdjustment adjustment in FindAllPending())
            {
                // Calculate adjustment
                double sum = 0;
                foreach (SupplierStationery supplier in adjustment.Stationery.SupplierStationeries)
                {
                    if (supplier.SupplierRank == 1)
                    {
                        sum += supplier.UnitPrice * Math.Abs(adjustment.QuantityAdjusted);
                        break;
                    }
                }

                // If adjustment >= $250, add to listAbove
                if (sum >= 250)
                    listAbove.Add(adjustment);
            }
            return listAbove;
        }

        public List<StockAdjustment> FindAllBelow250Pending()
        {

            SSAEntities context = new SSAEntities();
            List<StockAdjustment> listBelow = new List<StockAdjustment>(); // For appending adjustments >$250 

            // Loop through all Pending adjustments
            foreach (StockAdjustment adjustment in FindAllPending())
            {
                // Calculate adjustment
                double sum = 0;
                foreach (SupplierStationery supplier in adjustment.Stationery.SupplierStationeries)
                {
                    if (supplier.SupplierRank == 1)
                    {
                        sum += supplier.UnitPrice * Math.Abs(adjustment.QuantityAdjusted);
                        break;
                    }
                }

                // If adjustment > $250, add to listAbove
                if (sum < 250)
                    listBelow.Add(adjustment);
            }
            return listBelow;
        }

        public List<StockAdjustment> FindAllAdjustments()
        {
            SSAEntities context = new SSAEntities();
            List<StockAdjustment> listAdjustments = context.StockAdjustments.ToList<StockAdjustment>();
            return listAdjustments;
        }

        public List<StockAdjustment> FindAdjustmentsOfClerkWithFilter(int clerkId, string status)
        {
            SSAEntities context = new SSAEntities();
            if (status == "All")
            {
                return context.StockAdjustments.Where(x => x.ClerkEmployeeId == clerkId).ToList<StockAdjustment>();
            }
            List<StockAdjustment> listAdjustments = context.StockAdjustments
                                                                                      .Where(x => x.ClerkEmployeeId == clerkId && x.Status == status)
                                                                                      .ToList<StockAdjustment>();
            return listAdjustments;
        }

        public List<StockAdjustment> FindAllPending()
        {
            SSAEntities context = new SSAEntities();
            List<StockAdjustment> listRequests = context.StockAdjustments
                                                                                .Where(x => x.Status == "Pending")
                                                                                .ToList<StockAdjustment>();
            return listRequests;
        }

        public List<StockAdjustment> FindAllVouchers()
        {
            SSAEntities context = new SSAEntities();

            // List for appending one adjustment per voucher as they share the same
            // voucherNumber, dateApproved, status, and approvedBy
            List<StockAdjustment> listVouchers = new List<StockAdjustment>();

            // Get list of voucher numbers
            var listNum = context.StockAdjustments.Where(x => x.VoucherNumber != null)
                                              .Select(y => y.VoucherNumber).Distinct().ToList();

            // Get first adjustment item for each voucher
            foreach (int num in listNum)
                listVouchers.Add(context.StockAdjustments.Where(y => y.VoucherNumber == num).First());

            return listVouchers;
        }

        public int ReportAdjustment(int adjustmentId)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                StockAdjustment s = context.StockAdjustments
                                                      .Where(x => x.AdjustmentId == adjustmentId).FirstOrDefault();
                s.Status = "Reported";

                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 rows updated
            }
        }

        public int UpdateAdjustment(StockAdjustment adjustment)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                StockAdjustment s = context.StockAdjustments
                                                      .Where(x => x.AdjustmentId == adjustment.AdjustmentId).FirstOrDefault();
                s.ItemCode = adjustment.ItemCode;
                s.QuantityAdjusted = adjustment.QuantityAdjusted;
                s.Reason = adjustment.Reason;
                s.Status = adjustment.Status;
                s.ApproverRemarks = adjustment.ApproverRemarks;
                s.DateCreated = adjustment.DateCreated;
                s.DateApproved = adjustment.DateApproved;
                s.ClerkEmployeeId = adjustment.ClerkEmployeeId;
                s.ApprovedByEmployeeId = adjustment.ApprovedByEmployeeId;
                s.VoucherNumber = adjustment.VoucherNumber;

                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 rows updated
            }
        }

        public List<StockAdjustment> FindAllReported()
        {
            SSAEntities context = new SSAEntities();
            List<StockAdjustment> listRequests = context.StockAdjustments
                                                                                .Where(x => x.Status == "Reported")
                                                                                .ToList<StockAdjustment>();
            return listRequests;
        }

        public double FindTotalCostOfVoucher(int voucherNumber)
        {
            // Get list of adjustments
            List<StockAdjustment> listDetails = FindAllAdjustmentsOfVoucher(voucherNumber);

            // Calculate total cost
            double sum = 0;
            foreach (StockAdjustment detail in listDetails)
            {
                foreach (SupplierStationery supplier in detail.Stationery.SupplierStationeries)
                {
                    if (supplier.SupplierRank == 1)
                    {
                        sum += supplier.UnitPrice * Math.Abs(detail.QuantityAdjusted);
                        break;
                    }
                }
            }
            return sum;
        }

        public List<StockAdjustment> FindAllAdjustmentsOfVoucher(int voucherNumber)
        {
            SSAEntities context = new SSAEntities();
            List<StockAdjustment> listAdjustments = context.StockAdjustments
                                                                                      .Where(x => x.VoucherNumber == voucherNumber).ToList<StockAdjustment>();
            return listAdjustments;
        }

        public int FindLastVoucherNumber()
        {
            SSAEntities context = new SSAEntities();
            // If there are no existing vouchers, return seed
            if (context.StockAdjustments.Where(x => x.VoucherNumber != null).Count() == 0)
                return 30000000;
            else
                return (int) context.StockAdjustments.Max(x => x.VoucherNumber);
        }
    }
}