/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class SupplierService : ISupplierService
    {
        SSAEntities context = new SSAEntities();
        public int AddSupplier(Supplier supplier)
        {
            try
            {
                context.Suppliers.Add(supplier);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public List<Supplier> FindAllSuppliers()
        {
            List<Supplier> listSupplier = context.Suppliers.ToList();
            return listSupplier;
        }
        public Supplier FindSupplierById(string code)
        {
            Supplier supplier = context.Suppliers.Where(x => x.SupplierCode == code).FirstOrDefault();
            return supplier;
        }
        public int UpdateSupplier(Supplier supplier)
        {
            try
            {
                Supplier s = context.Suppliers.Where(x => x.SupplierCode == supplier.SupplierCode).FirstOrDefault();

                s.SupplierCode = supplier.SupplierCode;
                s.SupplierName = supplier.SupplierName;
                s.ContactName = supplier.ContactName;
                s.Phone = supplier.Phone;
                s.Fax = supplier.Fax;
                s.Address = supplier.Address;
                s.GSTRegistrationNo = supplier.GSTRegistrationNo;
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int DeleteSupplierById(string SupplierCode)
        {
            try
            {
                Supplier s = context.Suppliers.Where(x => x.SupplierCode == SupplierCode).FirstOrDefault();
                context.Suppliers.Remove(s);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int UpdateSupplier(string supplierCode, string supplierName, string contactName, int phone, int fax, string address, string gstRegistrationNo)
        {
            try
            {
                Supplier s = context.Suppliers.Where(x => x.SupplierCode == supplierCode).FirstOrDefault();

                s.SupplierCode = supplierCode;
                s.SupplierName = supplierName;
                s.ContactName = contactName;
                s.Phone = phone.ToString();
                s.Fax = fax.ToString();
                s.Address = address;
                s.GSTRegistrationNo = gstRegistrationNo;
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public double FindUnitPrice(string itemCode)
        {
            try
            {
                return context.SupplierStationeries.Where(x => x.ItemCode == itemCode && x.SupplierRank == 1).Select(x => x.UnitPrice).First();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}