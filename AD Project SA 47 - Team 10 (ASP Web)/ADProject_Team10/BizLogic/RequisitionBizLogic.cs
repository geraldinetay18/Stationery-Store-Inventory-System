/* Authors: 
 * Sun Chengyuan
 * Geraldine Tay*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ADProject_Team10.BizLogic
{
    using Models;
    using Services;
    public class RequisitionBizLogic
    {
        ITemporaryRoleService it = new TemporaryRoleService();
        IEmployeeService ie = new EmployeeService();
        IDepartmentService id = new DepartmentService();
        IRequisitionService ir = new RequisitionService();
        IRequisitionDetailService ird = new RequisitionDetailService();
        EmailBizLogic ebl = new EmailBizLogic();
        public int SubmitRequisition(int empId, List<RequisitionDetail> requisitiondetail)
        {
            try
            {
                // Build Requisition object
                Requisition r1 = new Requisition();
                r1.EmployeeId = empId;
                r1.RequisitionDate = DateTime.Now.Date;
                r1.RequisitionStatus = "Pending";

                // Store Requisition and Detail objects in database
                ir.CreateRequisition(r1);
                int reqId = ir.GetLastRequisitionId().RequisitionId;
                for (int i = 0; i < requisitiondetail.Count(); i++)
                {
                    requisitiondetail[i].RequisitionId = reqId;
                    ird.CreateRequisitionDetail(requisitiondetail[i]);
                }

                // Send email to Dept Head
                Employee requestor = ie.SearchEmployeeByEmpId(r1.EmployeeId);
                Employee deptHead = ie.SearchEmployeeByEmpId(id.SearchDepartmentByDeptId(requestor.DeptId).HeadId);
                Employee actingHead = FindCurrentActingHeadOfDept(requestor.DeptId, r1.RequisitionDate); 

                string subject = "Stationery Requisition - " + requestor.EmployeeName;
                string body = requestor.EmployeeTitle + ". " + requestor.EmployeeName + 
                                     " has just submitted a Requisition to you on the Stationery Store Inventory System (SSIS).\n" +
                                     "Please visit SSIS to review and approve the Requisition. Thank you.";
                ebl.SendEmail(deptHead, subject, body);
                if (actingHead != null) // Email Acting Head if any
                    ebl.SendEmail(actingHead, subject, body);

                return 1; // Success
            }
            catch (Exception e)
            {
                return 0; // Fail
            }
        }

        // Parse in date to cater for time lapse between submission and emailing
        Employee FindCurrentActingHeadOfDept(string deptId, DateTime date)
        {
            foreach (TemporaryRole tr in it.ListAllAssignment())
            {
                if (tr.Employee.DeptId == deptId  && tr.StartDate.Date <=  date && tr.EndDate >= date)
                    return tr.Employee;
            }
            return null;
        }
    }
}