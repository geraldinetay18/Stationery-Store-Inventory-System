/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;
using System;

namespace ADProject_Team10_WebApi.Services
{
    public interface ISupplierStationeryService
    {
        SupplierStationery FindSupplierStationery(int id);
        SupplierStationery FindSupplierStationery(string itemCode, string supplierCode);
        List<SupplierStationery> FindStationeryDetailOfSupplier(string supplierCode);
        int AddSupplierStationery (SupplierStationery supplierstationery);
        int UpdateSupplierStationery (int id, string supplierCode, string itemCode, double unitPrice, int rank);
        int DeleteSupplierStationery(string itemCode, string supplierCode);

    }
}