/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class RetrievalDetailService: IRetrievalDetailService
    {
        SSAEntities context = new SSAEntities();
        public RetrievalDetail FindRetrievalDetail(int id)
        {
            RetrievalDetail retrievalDetail = context.RetrievalDetails.Where(x => x.RetrievalDetailsId == id).First();
            return retrievalDetail;
        }
        public RetrievalDetail FindRetrievalDetail(string itemCode, int retrievalId)
        {
            RetrievalDetail retrievalDetail = context.RetrievalDetails.Where(x => x.ItemCode == itemCode && x.RetrievalId == retrievalId).First();
            return retrievalDetail;
        }
        public List<RetrievalDetail> FindAllRetrievalDetails()
        {
            List<RetrievalDetail> reDetailList = context.RetrievalDetails.ToList();
            return reDetailList;
        }
        public List<RetrievalDetail> FindRetrievalDetailsByItemCode(String itemCode)
        {
            List<RetrievalDetail> reDetailList = context.RetrievalDetails.Where(x => x.ItemCode == itemCode).ToList();
            return reDetailList;
        }
        public List<RetrievalDetail> FindRetrievalDetailsByRetrievalId(int id)
        {
            List<RetrievalDetail> reDetailList = context.RetrievalDetails.Where(x => x.RetrievalId == id).ToList();
            return reDetailList;
        }
        //need a method for breakdown by Dept - do in RequisitionService
        public int AddRetrievalDetail(RetrievalDetail retrievalDetail)
        {
            context.RetrievalDetails.Add(retrievalDetail);
            return context.SaveChanges();
        }
        public int UpdateRetrievalDetail(RetrievalDetail retrievalDetail)
        {
            RetrievalDetail re = context.RetrievalDetails.Where(x => x.RetrievalDetailsId == retrievalDetail.RetrievalDetailsId).First();
            re.QuantityRetrieved = retrievalDetail.QuantityRetrieved;
            re.Remark = retrievalDetail.Remark;
            return context.SaveChanges();
        }
        public int RemoveRetrievalDetail(int id)
        {
            RetrievalDetail retrievalDetail = context.RetrievalDetails.Where(x => x.RetrievalId == id).First();
            context.RetrievalDetails.Remove(retrievalDetail);
            return context.SaveChanges();
        }
    }
}