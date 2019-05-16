/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class DisbursementService : IDisbursementService
    {
        SSAEntities se = new SSAEntities();

        public int AddDisbursement(Disbursement disbursement)
        {
            try
            {
                se.Disbursements.Add(disbursement);
                se.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public List<Disbursement> FindByDateCreated(DateTime date)
        {
            return se.Disbursements.Where(d => d.DateCreated == date).ToList();

        }

        public List<Disbursement> FindByDateDisbursed(DateTime date)
        {
            return se.Disbursements.Where(d => d.DateDisbursed == date).ToList();
        }

        public List<Disbursement> FindByPeriodCreated(DateTime date1, DateTime date2)
        {
            return se.Disbursements.Where(d => d.DateCreated >= date1 && d.DateCreated <= date2).ToList();
        }

        public List<Disbursement> FindByPeriodDisbursed(DateTime date1, DateTime date2)
        {
            return se.Disbursements.Where(d => d.DateDisbursed >= date1 && d.DateDisbursed <= date2).ToList();
        }

        public List<Disbursement> FindByStatus(string status)
        {
            return se.Disbursements.Where(d => d.Status == status).ToList();
        }

        public List<Disbursement> FindByYear(string year)
        {
            return se.Disbursements.Where(d => d.DateDisbursed.ToString().Contains(year)).ToList();
        }

        public Disbursement FindEarliestDisbursement()
        {
            return se.Disbursements.OrderBy(x => x.DateDisbursed).FirstOrDefault();
        }

        public Disbursement FindLatestDisbursement()
        {
            return se.Disbursements.OrderByDescending(x => x.DateDisbursed).FirstOrDefault();
        }

        public List<Disbursement> GetAllDisbursement()
        {
            return se.Disbursements.ToList();
        }

        public Disbursement GetDisbursementById(int id)
        {
            return se.Disbursements.Where(d => d.DisbursementId == (id)).FirstOrDefault();
        }

        public Disbursement GetDisbursingDisburmentByDeptId(string id)
        {
            return se.Disbursements.Where(d => d.Status == "collected" && d.DeptId == id).FirstOrDefault();
        }

        public int UpdateDisbursement(Disbursement d)
        {
            Disbursement disbursement = se.Disbursements.Where(db => db.DisbursementId == d.DisbursementId).FirstOrDefault();
            if (disbursement != null)
            {               
                disbursement.Status = d.Status;
                disbursement.DateDisbursed = d.DateDisbursed;
                disbursement.Remark = d.Remark;
                disbursement.DisbursementDetails = d.DisbursementDetails;
                se.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        //edits
        public int FindQuan(string itemCode, string depId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<Disbursement> disbursements = se.Disbursements.Where(x => x.DeptId == depId).ToList();
                int sum = 0;
                foreach (Disbursement d in disbursements)
                {
                    if (d.Status == "Collected" && DateTime.Compare(fromDate.Date, ((DateTime)d.DateDisbursed).Date) < 0 && DateTime.Compare(toDate.Date, ((DateTime)d.DateDisbursed).Date) > 0)
                    {
                        DisbursementDetail disbursementDetail = se.DisbursementDetails.Where(x => x.DisbursementId == d.DisbursementId && x.ItemCode == itemCode).FirstOrDefault();
                        if (disbursementDetail != null)
                        {

                            sum += disbursementDetail.QuantityReceived;
                            return sum;
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<DisbursementDetail> GetDisbursementStationeryByDeptId_Date(string depId, DateTime fromdate, DateTime toDate)
        {
            SSAEntities ssa = new SSAEntities();
            try
            {
                // List<Disbursement> d= ssa.Disbursements.Where(x=> x.DeptId== depId && x.)
                List<DisbursementDetail> list = ssa.DisbursementDetails.Where(x => x.Disbursement.DeptId.Equals(depId)
                 && x.Disbursement.DateDisbursed >= fromdate && x.Disbursement.DateDisbursed <= toDate
                 && x.DisbursementId == x.Disbursement.DisbursementId && x.Disbursement.Status == "Collected").ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int UpdateDisbursementStatus(string depId, DateTime fromdate, DateTime toDate)
        {
            SSAEntities ssa = new SSAEntities();
            List<string> departmentId = new List<string>();
            try
            {
                List<Disbursement> disbursements = se.Disbursements.Where(x => x.DeptId == depId).ToList();
                foreach (Disbursement d in disbursements)
                {
                    if (d.Status == "Collected" && DateTime.Compare(fromdate.Date, ((DateTime)d.DateDisbursed).Date) < 0 && DateTime.Compare(toDate.Date, ((DateTime)d.DateDisbursed).Date) > 0)
                    {
                        DisbursementDetail disbursementDetail = se.DisbursementDetails.Where(x => x.DisbursementId == d.DisbursementId).FirstOrDefault();
                        if (disbursementDetail != null)
                        {
                            disbursementDetail.Disbursement.Status = "Charged";
                            se.SaveChanges();
                            return 1;
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<Disbursement> GetDisbursementsByDeptId(string deptId)
        {
            return se.Disbursements.Where(x => x.DeptId == deptId).ToList();
        }
    }
}