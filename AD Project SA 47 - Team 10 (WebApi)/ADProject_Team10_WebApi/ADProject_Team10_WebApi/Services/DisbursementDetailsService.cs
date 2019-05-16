/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class DisbursementDetailsService : IDisbursementDetailsService
    {
        SSAEntities se = new SSAEntities();
        public DisbursementDetail GetDisbursementDetailByIdAndItemcode(int disbursementId, string itemCode)
        {
            return se.DisbursementDetails.Where(dd => dd.DisbursementId == disbursementId  && dd.ItemCode == itemCode).FirstOrDefault();
        }

        public List<DisbursementDetail> GetDisbursementDetailsByDisbursementId(int id)
        {
            return se.DisbursementDetails.Where(x => x.DisbursementId == id).ToList();

        }

        public List<DisbursementDetail> ListAllDisbursementDetails()
        {
            return se.DisbursementDetails.ToList();
        }

        public List<DisbursementDetail> ListByItemCode(string ItemCode)
        {
            return se.DisbursementDetails.Where(dd => dd.ItemCode == ItemCode).ToList();
        }

        public List<DisbursementDetail> ListByItemCode_StartDate(string ItemCode, DateTime start)
        {
            List<DisbursementDetail> ddList = new List<DisbursementDetail>();
            List<DisbursementDetail> ddListAfterStartDate = new List<DisbursementDetail>();
            // List<Disbursement> dList = em.Disbursements.Where(dd => dd.DateDisbursed >= start).ToList();
            ddList = se.DisbursementDetails.Where(dd => dd.ItemCode == ItemCode).ToList();
            foreach (DisbursementDetail d in ddList)
            {
                if (se.Disbursements.Where(x => x.DisbursementId == d.DisbursementId && x.DateDisbursed >= start).FirstOrDefault() != null)
                {
                    ddListAfterStartDate.Add(d);
                }
            }
            return ddListAfterStartDate;
        }

        public List<DisbursementDetail> SearchByStationeryItem(Stationery item)
        {
            return se.DisbursementDetails.Where(dd => dd.ItemCode == item.ItemCode).ToList();
        }

        public int AddDisbursementDetail(DisbursementDetail disbursementDetail)
        {
            se.DisbursementDetails.Add(disbursementDetail);
            return se.SaveChanges();
        }

        public List<DisbursementDetail> SearchDisbDetsForNestedList(int storeClerkId, string status, string itemCode)
        {
            List<Disbursement> disbursements = se.Disbursements.Where(x => x.StoreClerkId == storeClerkId && x.Status == status).ToList();
            List<DisbursementDetail> disbursementDetails = new List<DisbursementDetail>();
            foreach (Disbursement disbursement in disbursements)
            {
                disbursementDetails.AddRange(se.DisbursementDetails.Where(x => x.DisbursementId == disbursement.DisbursementId && x.ItemCode == itemCode).ToList());
            }
            return disbursementDetails;
        }

        public DisbursementDetail SearchDisbursementDetailById(int id) //ok
        {
            return se.DisbursementDetails.Where(x => x.DisbursementDetailsId == id).First();
        }
        public int UpdateDisbursementDetail(DisbursementDetail disbursementDetail)
        {
            DisbursementDetail dd = se.DisbursementDetails.Where(x => x.DisbursementDetailsId == disbursementDetail.DisbursementDetailsId).First();
            dd.QuantityRequested = disbursementDetail.QuantityRequested;
            dd.Remark = disbursementDetail.Remark;
            return se.SaveChanges();
        }

        public List<DisbursementDetail> SearchDisbDetByDisbId(int disbId)
        {
            return se.DisbursementDetails.Where(x => x.DisbursementId == disbId).ToList();
        }

    }
}