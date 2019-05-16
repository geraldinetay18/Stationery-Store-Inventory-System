/* Author: Sun Chengyuan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    public class RequisitionDetailService : IRequisitionDetailService
    {
        // view requisitiondetail by statTransId
        public List<RequisitionDetail> GetRequisitionDetail(int StatTransId)
        {
            using (SSAEntities e = new SSAEntities())
            {
                return new SSAEntities().RequisitionDetails.Where(p => p.StatTransId == StatTransId).ToList();
            }
        }
        public RequisitionDetail GetRequisitionDetailBySId(int StatTransId)
        {
            using (SSAEntities e = new SSAEntities())
            {
                return new SSAEntities().RequisitionDetails.Where(p => p.StatTransId == StatTransId).FirstOrDefault();
            }
        }
        //------------------------------------------------

        public List<RequisitionDetail> SearchRequisitionDetailByRequisitionId(int requisitionId)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.RequisitionDetails.Where(x => x.RequisitionId == requisitionId).ToList();
            }
        }
        public List<RequisitionDetail> SearchRequisitionDetailByItemCode(string itemCode)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.RequisitionDetails.Where(x => x.ItemCode.ToUpper().Contains(itemCode.Trim().ToUpper())).ToList();
            }

        }

        public void UpdateRequisitionDetailQuantityRequest(int SI, int q)
        {
            using (SSAEntities s = new SSAEntities())
            {
                RequisitionDetail rd = s.RequisitionDetails.Where(x => x.StatTransId == SI).FirstOrDefault();
                rd.QuantityRequest = q;
                s.SaveChanges();
            }
        }


        //get itemcode in the requisitiondetail
        public RequisitionDetail GetRequisitionDetailByRequisitionIdAndItemCode(int RequisitionId, string itemCode)
        {
            using (SSAEntities s = new SSAEntities())
            {
                return s.RequisitionDetails
                    .Where(r => r.RequisitionId == RequisitionId && r.ItemCode == itemCode)
                    .FirstOrDefault<RequisitionDetail>();
            }
        }
        //-----------------------------------------
        //delete requisitionDetail
        public void DeleteRequisitionDetail(int SI)
        {
            using (SSAEntities s = new SSAEntities())
            {
                RequisitionDetail r = s.RequisitionDetails.Where(x => x.StatTransId == SI).FirstOrDefault();
                s.RequisitionDetails.Remove(r);
                s.SaveChanges();
            }
        }
        //----------------------------------
        public int CreateRequisitionDetail(RequisitionDetail r)
        {
            using (SSAEntities s = new SSAEntities())
            {
                s.RequisitionDetails.Add(r);
                return s.SaveChanges();
            }
        }
        public RequisitionDetail GetLastStatTransId()
        {
            using (SSAEntities s = new SSAEntities())
            {
                RequisitionDetail rd = s.RequisitionDetails.OrderByDescending(x => x.StatTransId).FirstOrDefault();
                return rd;
            }
        }

        public int GetTotalQuantityByRequisitionId(int RequisitionId)
        {
            List<RequisitionDetail> lrd = SearchRequisitionDetailByRequisitionId(RequisitionId);
            if (lrd.Count > 0)
            {
                int sum = 0;
                foreach (RequisitionDetail rd in lrd)
                    sum += rd.QuantityRequest;
                return sum;
            }
            else
                return 0;
        }
    }
}