/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface IStockAdjustmentService
    {
        int AddStockAdjustment(StockAdjustment adjustment);
        List<StockAdjustment> FindAllAdjustments();
        StockAdjustment FindAdjustmentById(int adjustmentId);
        List<StockAdjustment> FindAdjustmentsOfClerkWithFilter(int clerkId, string status);
        List<StockAdjustment> FindAllAdjustmentsOfVoucher(int voucherNumber);
        double FindTotalCostOfVoucher(int voucherNumber);
        List<StockAdjustment> FindAllPending();
        List<StockAdjustment> FindAllAbove250Pending();
        List<StockAdjustment> FindAllBelow250Pending();
        List<StockAdjustment> FindAllReported();
        List<StockAdjustment> FindAllVouchers();
        int FindLastVoucherNumber();
        int UpdateAdjustment(StockAdjustment adjustment);
        int DeleteAdjustmentById(int adjustmentId);
        int ApproveAdjustment(StockAdjustment adjustment);
        int RejectAdjustment(StockAdjustment adjustment);
        int ReportAdjustment(int adjustmentId);
    }
}
