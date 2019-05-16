/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10.BizLogic
{
    using Models;
    using Services;
    public class SupplierBizLogic
    {
        ISupplierService suService = new SupplierService();
        ISupplierStationeryService suStaService = new SupplierStationeryService();
        IStationeryService stService = new StationeryService();

        public Supplier FindSupplierById(string id)
        {
            return suService.FindSupplierById(id);
        }

        public List<SupplierStationery> FindStationeryDetailOfSupplier(string supplierCode)
        {
            return suStaService.FindStationeryDetailOfSupplier(supplierCode);
        }
        public string FindDescription(string itemCode)
        {
            return stService.FindStationeryById(itemCode).Description;
        }

        public Stationery FindStationeryById(string code)
        {
            return stService.FindStationeryById(code);
        }
    }
}