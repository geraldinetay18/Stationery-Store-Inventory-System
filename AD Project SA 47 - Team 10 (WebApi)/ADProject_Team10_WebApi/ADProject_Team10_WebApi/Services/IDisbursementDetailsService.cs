/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    interface IDisbursementDetailsService
    {
        List<DisbursementDetail> SearchByStationeryItem(Stationery item);
        List<DisbursementDetail> ListByItemCode(string ItemCode);
        List<DisbursementDetail> ListByItemCode_StartDate(string ItemCode, DateTime start);
        List<DisbursementDetail> ListAllDisbursementDetails();
        DisbursementDetail GetDisbursementDetailByIdAndItemcode(int disbursementId, String itemCode);
        List<DisbursementDetail> GetDisbursementDetailsByDisbursementId(int id);
        int AddDisbursementDetail(DisbursementDetail disbursementDetail);
        List<DisbursementDetail> SearchDisbDetsForNestedList(int storeClerkId, string status, string itemCode);
        DisbursementDetail SearchDisbursementDetailById(int id);
        int UpdateDisbursementDetail(DisbursementDetail disbursementDetail);
        List<DisbursementDetail> SearchDisbDetByDisbId(int disbId);
    }
}
