/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADProject_Team10.BizLogic
{
    public class StockManagementBizLogic
    {
        public static Stationery getStationery(String itemCode)
        {
            SSAEntities ssae = new SSAEntities();
            Stationery stationery = ssae.Stationeries.Where(x => x.ItemCode.Contains(itemCode)).FirstOrDefault();
            return stationery;
        }

        public static List<StockMgmtMaintain> getStockManagements(String itemCode, int storeClerkId)
        {
            List<StockMgmtMaintain> stockManagements = new List<StockMgmtMaintain>();

            String connectionString = "Data Source=.;Initial Catalog=SSIS10;Integrated Security=True";

            var connectionSource = new SqlConnection(connectionString);
            connectionSource.Open();

            String querySource = " SELECT [SSIS10].[dbo].[StockManagement].[StockManagementId], [SSIS10].[dbo].[StockManagement].[Source], [SSIS10].[dbo].[StockManagement].[SourceId] " +
                " FROM [SSIS10].[dbo].[StockManagement] " +
                " WHERE [SSIS10].[dbo].[StockManagement].[ItemCode] = '" + itemCode + "' AND [SSIS10].[dbo].[StockManagement].[StoreClerkId] = " + storeClerkId + " " +
                " ORDER BY [SSIS10].[dbo].[StockManagement].[Date] DESC ";
            var commandSource = new SqlCommand(querySource, connectionSource);
            var readerSource = commandSource.ExecuteReader();

            while (readerSource.Read())
            {
                int stockManagementId = readerSource.GetInt32(0);
                String source = readerSource.GetString(1);
                int sourceId = readerSource.GetInt32(2);

                if (source.Equals("ORD"))
                {
                    var connectionOrder = new SqlConnection(connectionString);
                    connectionOrder.Open();

                    String queryOrder = " SELECT [SSIS10].[dbo].[StockManagement].[Date], [SSIS10].[dbo].[Supplier].[SupplierName], [SSIS10].[dbo].[StockManagement].[QtyAdjusted], [SSIS10].[dbo].[StockManagement].[Balance] " +
                        " FROM [SSIS10].[dbo].[StockManagement] INNER JOIN [SSIS10].[dbo].[Order] ON [SSIS10].[dbo].[Order].[PoNumber] = [SSIS10].[dbo].[StockManagement].[SourceId] " +
                        " INNER JOIN [SSIS10].[dbo].[Supplier] ON [SSIS10].[dbo].[Supplier].[SupplierCode] = [SSIS10].[dbo].[Order].[SupplierCode] " +
                        " WHERE [SSIS10].[dbo].[StockManagement].[StockManagementId] = " + stockManagementId;
                    var commandOrder = new SqlCommand(queryOrder, connectionOrder);
                    var readerOrder = commandOrder.ExecuteReader();

                    while (readerOrder.Read())
                    {
                        StockMgmtMaintain stockManagement = new StockMgmtMaintain();

                        DateTime date = readerOrder.GetDateTime(0);
                        String dateString = date.ToString("dd MMM yyyy");

                        String supplierName = readerOrder.GetString(1);
                        String deptName = "";
                        int qtyAdjusted = readerOrder.GetInt32(2);
                        int balance = readerOrder.GetInt32(3);
                        String reason = "";
                        String supl_dept = "Supplier - " + readerOrder.GetString(1);

                        stockManagement.Date = dateString;
                        stockManagement.SuplName = supplierName;
                        stockManagement.DeptName = deptName;
                        stockManagement.QtyAdjusted = qtyAdjusted;
                        stockManagement.Balance = balance;
                        stockManagement.Reason = reason;
                        stockManagement.Supl_dept = supl_dept;

                        stockManagements.Add(stockManagement);
                    }

                    connectionOrder.Close();
                }

                if (source.Equals("DIS"))
                {
                    var connectionDisbursement = new SqlConnection(connectionString);
                    connectionDisbursement.Open();

                    String queryDisbursement = " SELECT [SSIS10].[dbo].[StockManagement].[Date], [SSIS10].[dbo].[Department].[DeptName], [SSIS10].[dbo].[StockManagement].[QtyAdjusted], [SSIS10].[dbo].[StockManagement].[Balance] " +
                        " FROM [SSIS10].[dbo].[StockManagement] " + 
                        "INNER JOIN [SSIS10].[dbo].[DisbursementDetails] ON [SSIS10].[dbo].[DisbursementDetails].[DisbursementDetailsId] = [SSIS10].[dbo].[StockManagement].[SourceId] " +
                        "INNER JOIN [SSIS10].[dbo].[Disbursement] ON [SSIS10].[dbo].[Disbursement].[DisbursementId] = [SSIS10].[dbo].[DisbursementDetails].[DisbursementId]" +
                        " INNER JOIN [SSIS10].[dbo].[Department] ON [SSIS10].[dbo].[Department].[DeptId] = [SSIS10].[dbo].[Disbursement].[DeptId] " +
                        " WHERE [SSIS10].[dbo].[StockManagement].[StockManagementId] = " + stockManagementId;
                    var commandDisbursement = new SqlCommand(queryDisbursement, connectionDisbursement);
                    var readerDisbursement = commandDisbursement.ExecuteReader();

                    while (readerDisbursement.Read())
                    {
                        StockMgmtMaintain stockManagement = new StockMgmtMaintain();

                        DateTime date = readerDisbursement.GetDateTime(0);
                        String dateString = date.ToString("dd MMM yyyy");

                        String supplierName = "";
                        String deptName = readerDisbursement.GetString(1);
                        int qtyAdjusted = readerDisbursement.GetInt32(2);
                        int balance = readerDisbursement.GetInt32(3);
                        String reason = "";
                        String supl_dept = "Department - " + readerDisbursement.GetString(1);

                        stockManagement.Date = dateString;
                        stockManagement.SuplName = supplierName;
                        stockManagement.DeptName = deptName;
                        stockManagement.QtyAdjusted = qtyAdjusted;
                        stockManagement.Balance = balance;
                        stockManagement.Reason = reason;
                        stockManagement.Supl_dept = supl_dept;

                        stockManagements.Add(stockManagement);
                    }

                    connectionDisbursement.Close();
                }

                if (source.Equals("ADJ"))
                {
                    var connectionAdjustment = new SqlConnection(connectionString);
                    connectionAdjustment.Open();

                    String queryDisbursement = " SELECT [SSIS10].[dbo].[StockManagement].[Date], [SSIS10].[dbo].[StockAdjustments].[Reason], [SSIS10].[dbo].[StockManagement].[QtyAdjusted], [SSIS10].[dbo].[StockManagement].[Balance], [SSIS10].[dbo].[StockAdjustments].[VoucherNumber] " +
                        " FROM [SSIS10].[dbo].[StockManagement] INNER JOIN [SSIS10].[dbo].[StockAdjustments] ON [SSIS10].[dbo].[StockAdjustments].[AdjustmentId] = [SSIS10].[dbo].[StockManagement].[SourceId] " +
                        " WHERE [SSIS10].[dbo].[StockManagement].[StockManagementId] = " + stockManagementId;
                    var commandDisbursement = new SqlCommand(queryDisbursement, connectionAdjustment);
                    var readerDisbursement = commandDisbursement.ExecuteReader();

                    while (readerDisbursement.Read())
                    {
                        StockMgmtMaintain stockManagement = new StockMgmtMaintain();

                        DateTime date = readerDisbursement.GetDateTime(0);
                        String dateString = date.ToString("dd MMM yyyy");

                        String supplierName = "";
                        String deptName = "";
                        int qtyAdjusted = readerDisbursement.GetInt32(2);
                        int balance = readerDisbursement.GetInt32(3);
                        String reason = readerDisbursement.GetString(1);
                        String supl_dept = "Stock Adjustment - " + readerDisbursement.GetInt32(4);

                        stockManagement.Date = dateString;
                        stockManagement.SuplName = supplierName;
                        stockManagement.DeptName = deptName;
                        stockManagement.QtyAdjusted = qtyAdjusted;
                        stockManagement.Balance = balance;
                        stockManagement.Reason = reason;
                        stockManagement.Supl_dept = supl_dept;

                        stockManagements.Add(stockManagement);
                    }

                    connectionAdjustment.Close();
                }
            }

            connectionSource.Close();

            return stockManagements;
        }
       
        public static List<SupplierStationery> getSupplierStationeries(String itemCode)
        {
            SSAEntities ssae = new SSAEntities();
            List<SupplierStationery> supplierStationeries = ssae.SupplierStationeries.Where(x => x.ItemCode.Contains(itemCode)).ToList<SupplierStationery>();
            return supplierStationeries;
        }

        public static Supplier getSupplier(String supplierCode)
        {
            SSAEntities ssae = new SSAEntities();
            Supplier supplier = ssae.Suppliers.Where(x => x.SupplierCode.Contains(supplierCode)).FirstOrDefault();
            return supplier;
        }

        public static List<StockAdjMaintain> getStockAdjustments(String itemCode)
        {
            List<StockAdjMaintain> stockAdjustments = new List<StockAdjMaintain>();

            String connectionString = "Data Source=.;Initial Catalog=SSIS10;Integrated Security=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            String query = " SELECT [SSIS10].[dbo].[StockAdjustments].DateApproved, [SSIS10].[dbo].[Department].DeptName, [SSIS10].[dbo].[Supplier].SupplierName, [SSIS10].[dbo].[StockAdjustments].QuantityAdjusted, [SSIS10].[dbo].[StockAdjustments].Reason " +
                " FROM [SSIS10].[dbo].[Order] INNER JOIN[SSIS10].[dbo].[OrderDetails] ON[SSIS10].[dbo].[Order].PoNumber = [SSIS10].[dbo].[OrderDetails].PoNumber " +
                " INNER JOIN [SSIS10].[dbo].[StockAdjustments] ON[SSIS10].[dbo].[StockAdjustments].ItemCode = [SSIS10].[dbo].[OrderDetails].ItemCode " +
                " INNER JOIN [SSIS10].[dbo].[DisbursementDetails] ON[SSIS10].[dbo].[DisbursementDetails].ItemCode = [SSIS10].[dbo].[OrderDetails].ItemCode " +
                " INNER JOIN [SSIS10].[dbo].[Disbursement] ON[SSIS10].[dbo].[Disbursement].DisbursementId = [SSIS10].[dbo].[DisbursementDetails].DisbursementId " +
                " INNER JOIN [SSIS10].[dbo].[Department] ON[SSIS10].[dbo].[Department].DeptId = [SSIS10].[dbo].[Disbursement].DeptId " +
                " INNER JOIN [SSIS10].[dbo].[Supplier] ON[SSIS10].[dbo].[Supplier].SupplierCode = [SSIS10].[dbo].[Order].SupplierCode " +
                " WHERE [SSIS10].[dbo].[StockAdjustments].Status = 'Approved' AND [SSIS10].[dbo].[StockAdjustments].ItemCode = '" + itemCode + "' " +
                " ORDER BY [SSIS10].[dbo].[StockAdjustments].DateApproved ASC ";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                StockAdjMaintain stockAdjustment = new StockAdjMaintain();

                DateTime dateApproved = reader.GetDateTime(0);
                String dateAprrovedString = dateApproved.ToString("dd MMM yyyy");

                String deptName = reader.GetString(1);
                String supplierName = reader.GetString(2);
                int quantityAdjusted = reader.GetInt32(3);
                String reason = reader.GetString(4);

                stockAdjustment.DateApproved = dateAprrovedString;
                stockAdjustment.DepartmentName = deptName;
                stockAdjustment.SupplierName = supplierName;
                stockAdjustment.QuantityAdjusted = quantityAdjusted;
                stockAdjustment.Reason = reason;

                stockAdjustments.Add(stockAdjustment);
            }
            connection.Close();

            return stockAdjustments;
        }
    }
}
  
