/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10.BizLogic
{
    using Models;
    using Services;
    public class StockBizLogic
    {
        IStationeryService sService = new StationeryService();

        public List<Stationery> FindAllLowStock()
        {
            return sService.FindAllLowStock();
        }
    }
}