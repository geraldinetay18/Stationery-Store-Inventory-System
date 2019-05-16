/* Author: Sun Chengyuan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    public class RequisitionService:IRequisitionService
    {
        // view submitted requisition
        public List<Requisition> ListAllRequisition()
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.ToList<Requisition>();
            }
        }
        //----------------------------
        public Requisition SearchRequisitionbyID(int RequisitionId)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.RequisitionId == RequisitionId).FirstOrDefault();
            }
        }
        public List<Requisition> SearchRequisitionbyStatus(string RequisitionStatus)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.RequisitionStatus.ToUpper().Contains(RequisitionStatus.Trim().ToUpper())).ToList();
            }
        }
        // search requisition by employeeId
        public List<Requisition> SearchRequisitionbyEmployeeId(int employeeId)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.EmployeeId==employeeId).ToList();
            }
        }
        //------------------------------
        
        // view requisition history by two date
        public List<Requisition> SearchRequisitionbyTwoDate(DateTime RequisitionDate1,DateTime RequisitionDate2)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.RequisitionDate >= RequisitionDate1 && x.RequisitionDate <= RequisitionDate2).ToList();
            }
        }

        //------------------------------------------------

        public List<Requisition> SearchRequisitionByDate(DateTime time)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.RequisitionDate== time).ToList<Requisition>();
            }
        }

        public Requisition GetRequisition(int RI)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
            }
        }
        public Requisition GetLastRequisitionId()
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition r = s.Requisitions.OrderByDescending(x => x.RequisitionId).FirstOrDefault();
                return r;
            }
        }

        // submit requisition 
        public int CreateRequisition(Requisition r)
        {
            using (SSAEntities s = new SSAEntities())
            {
                s.Requisitions.Add(r);
                return s.SaveChanges();
            }
        }
        //-----------------------------

        //approve and reject requisition
        public void UpdateRequisitionStatus(int RI ,string Status)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition R=s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
                R.RequisitionStatus = Status;
                s.SaveChanges();
            }

        }
        public void UpdateRequisitionApprovedBy(int RI, int Id)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition R = s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
                R.ApprovedByEmployeeId = Id;
                s.SaveChanges();
            }

        }
        public void UpdateRequisitionRemark(int RI, string remark)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition R = s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
                R.Remark = remark;
                s.SaveChanges();
            }

        }

        public void UpdateRequisitionEmployeeId(int RI, int employeeId)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition R = s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
                R.EmployeeId = employeeId;
                s.SaveChanges();
            }
        }
        //------------------------



        public int UpdateRequisitionDetail(Requisition r)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition req = s.Requisitions.Where(x => x.RequisitionId == r.RequisitionId).FirstOrDefault();

                req.RequisitionStatus = r.RequisitionStatus;
                req.RequisitionDate = r.RequisitionDate;
                req.Remark = r.Remark;
                req.EmployeeId = r.EmployeeId;
                return s.SaveChanges();

            }

        }

        public void UpdateRequisitionApproval(int RI,int approid)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition R = s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
                R.ApprovedByEmployeeId = approid;
                s.SaveChanges();

            }

        }
        public void DeleteRequisition(int RI)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition r = s.Requisitions.Where(x => x.RequisitionId == RI).FirstOrDefault();
                s.Requisitions.Remove(r);
                s.SaveChanges();
            }
        }
        public List<Requisition> OrderRequisitionByDate()
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.OrderBy(x =>x.RequisitionDate).ToList();
            }
        }
        public List<Requisition> OrderRequisitionByStatus()
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.OrderBy(x => x.RequisitionStatus).ToList();
            }
        }

        public List<Requisition> GetRequisitionListOfDept(string deptId)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.Employee.DeptId == deptId).ToList();
            }
        }

        public List<Requisition> GetPendingRequisitionListOfDept(string deptId)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.Employee.DeptId == deptId && x.RequisitionStatus.ToLower() == "pending").ToList();
            }
        }

        public int UpdateReqStatus(int reqId, string status)
        {
            using (SSAEntities s = new SSAEntities())
            {
                Requisition req = s.Requisitions.Where(x => x.RequisitionId == reqId).FirstOrDefault();
                req.RequisitionStatus = status;
                return s.SaveChanges();
            }
        }
        public List<Requisition> FindReqsByDate(DateTime from, DateTime to)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.Requisitions.Where(x => x.RequisitionDate <= to && x.RequisitionDate >= from).ToList();
            }
        }
    }
}