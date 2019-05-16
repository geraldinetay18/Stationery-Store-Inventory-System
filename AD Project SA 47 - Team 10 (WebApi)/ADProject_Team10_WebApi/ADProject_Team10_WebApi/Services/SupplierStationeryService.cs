/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class SupplierStationeryService : ISupplierStationeryService
    {
        SSAEntities context = new SSAEntities();
        public SupplierStationery FindSupplierStationery(int id)
        {
            SupplierStationery supplierstationery = context.SupplierStationeries.Where(x => x.SupplierStationeryId == id).First();
            return supplierstationery;
        }
        public SupplierStationery FindSupplierStationery(string itemCode, string supplierCode)
        {
            SupplierStationery supplierstationery = context.SupplierStationeries.Where(x => x.ItemCode == itemCode && x.SupplierCode == supplierCode).First();
            return supplierstationery;
        }
        public int AddSupplierStationery(SupplierStationery supplierstationery)
        {
            context.SupplierStationeries.Add(supplierstationery);
            return context.SaveChanges();
        }
        public int UpdateSupplierStationery(int id, string supplierCode, string itemCode, double unitPrice, int rank)
        {
            SupplierStationery ss = context.SupplierStationeries.Where(x => x.SupplierStationeryId == id).First();
            ss.SupplierCode = supplierCode;
            ss.ItemCode = itemCode;
            ss.UnitPrice = unitPrice;
            ss.SupplierRank = rank;
            return context.SaveChanges();
        }
        public int DeleteSupplierStationery(string itemCode, string supplierCode)
        {
            SupplierStationery supplierstationery = context.SupplierStationeries.Where(x => x.ItemCode == itemCode && x.SupplierCode == supplierCode).First();
            context.SupplierStationeries.Remove(supplierstationery);
            return context.SaveChanges();
        }

        public List<SupplierStationery> FindStationeryDetailOfSupplier(string supplierCode)
        {
            return context.SupplierStationeries.Where(x => x.SupplierCode == supplierCode).ToList();
        }
    }
}
