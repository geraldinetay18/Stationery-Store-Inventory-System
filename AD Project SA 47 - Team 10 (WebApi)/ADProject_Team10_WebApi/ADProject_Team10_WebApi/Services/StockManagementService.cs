/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    public class StockManagementService : IStockManagementService
    {
        public int AddStockManagement(StockManagement sm)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.StockManagements.Add(sm);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        public StockManagement FindStockManagementBySourceId(string source, int id)
        {
            SSAEntities context = new SSAEntities();
            return context.StockManagements.Where(x => x.Source == source && x.SourceId == id).First();
        }
        public int UpdateStockManagement(StockManagement sm)
        {
            SSAEntities context = new SSAEntities();
            StockManagement s = context.StockManagements.Where(x => x.StockManagementId == sm.StockManagementId).First();
            s.Date = sm.Date;
            s.StoreClerkId = sm.StoreClerkId;
            s.QtyAdjusted = sm.QtyAdjusted;
            s.Balance = sm.Balance;
            return context.SaveChanges();
        }
    }
}