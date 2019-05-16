/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;
using System;

namespace ADProject_Team10_WebApi.Services
{
    public interface ISupplierService
    {
        int AddSupplier(Supplier supplier);
        List<Supplier> FindAllSuppliers();
        Supplier FindSupplierById(string SupplierCode);
        int UpdateSupplier(string supplierCode, string supplierName, string contactName, int phone, int fax, string address, string gstRegistrationNo);
        int DeleteSupplierById(string SupplierCode);
        double FindUnitPrice(string itemCode);
    }
}