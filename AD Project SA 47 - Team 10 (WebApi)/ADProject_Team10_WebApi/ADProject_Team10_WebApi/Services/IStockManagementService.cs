/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface IStockManagementService
    {
        int AddStockManagement(StockManagement sm);
        StockManagement FindStockManagementBySourceId(string source, int id);
        int UpdateStockManagement(StockManagement sm);
    }
}
