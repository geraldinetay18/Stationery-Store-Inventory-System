/* Author: Sun Chengyuan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface IRequisitionDetailService
    {
        List<RequisitionDetail> GetRequisitionDetail(int StatTransId);
        RequisitionDetail GetRequisitionDetailBySId(int StatTransId);
        List<RequisitionDetail> SearchRequisitionDetailByRequisitionId(int requisitionId);
        List<RequisitionDetail> SearchRequisitionDetailByItemCode(string itemCode);
        void UpdateRequisitionDetailQuantityRequest(int SI, int q);
        RequisitionDetail GetRequisitionDetailByRequisitionIdAndItemCode(int RequisitionId, string itemCode);
        void DeleteRequisitionDetail(int SI);
        int CreateRequisitionDetail(RequisitionDetail rd);
        RequisitionDetail GetLastStatTransId();
        int GetTotalQuantityByRequisitionId(int RequisitionId);
    }
}
