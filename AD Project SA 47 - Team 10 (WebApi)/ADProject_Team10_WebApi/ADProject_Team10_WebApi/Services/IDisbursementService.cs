/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    interface IDisbursementService
    {
        List<Disbursement> GetAllDisbursement();
        Disbursement GetDisbursementById(int id);
        Disbursement GetDisbursingDisburmentByDeptId(String id);
        List<Disbursement> FindByDateDisbursed(DateTime date);
        List<Disbursement> FindByDateCreated(DateTime date);
        List<Disbursement> FindByYear(string year);
        List<Disbursement> FindByPeriodDisbursed(DateTime date1, DateTime date2);
        List<Disbursement> FindByPeriodCreated(DateTime date1, DateTime date2);
        List<Disbursement> FindByStatus(string status);
        int UpdateDisbursement(Disbursement d);
        
        int AddDisbursement(Disbursement disbursement);
        Disbursement FindEarliestDisbursement();
        Disbursement FindLatestDisbursement();
        List<DisbursementDetail> GetDisbursementStationeryByDeptId_Date(string depId, DateTime fromdate, DateTime toDate);
        int FindQuan(string itemCode, string depId, DateTime fromDate, DateTime toDate);
        int UpdateDisbursementStatus(string depId, DateTime fromdate, DateTime toDate);
        List<Disbursement> GetDisbursementsByDeptId(string deptId);
    }
}
