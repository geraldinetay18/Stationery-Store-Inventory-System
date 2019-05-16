/* Author: Sun Chengyuan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface IRequisitionService
    {
        List<Requisition> ListAllRequisition();
        Requisition SearchRequisitionbyID(int RequisitionId);
        List<Requisition> SearchRequisitionbyStatus(string RequisitionStatus);
        List<Requisition> SearchRequisitionbyEmployeeId(int employeeId);
        List<Requisition> SearchRequisitionbyTwoDate(DateTime RequisitionDate1, DateTime RequisitionDate2);
        List<Requisition> SearchRequisitionByDate(DateTime time);
        Requisition GetRequisition(int RI);
        int CreateRequisition(Requisition newR);
        void UpdateRequisitionStatus(int RI, string Status);
        void UpdateRequisitionRemark(int RI, string remark);
        void UpdateRequisitionEmployeeId(int RI, int employeeId);
        int UpdateRequisitionDetail(Requisition r);
        void UpdateRequisitionApprovedBy(int RI, int Id);
        void UpdateRequisitionApproval(int RI, int approid);
        void DeleteRequisition(int RI);
        List<Requisition> OrderRequisitionByDate();
        List<Requisition> OrderRequisitionByStatus();
        Requisition GetLastRequisitionId();
        List<Requisition> GetRequisitionListOfDept(string deptId);
        List<Requisition> GetPendingRequisitionListOfDept(string deptId);
        int UpdateReqStatus(int reqId, string status);
        List<Requisition> FindReqsByDate(DateTime from, DateTime to);
    }
}
