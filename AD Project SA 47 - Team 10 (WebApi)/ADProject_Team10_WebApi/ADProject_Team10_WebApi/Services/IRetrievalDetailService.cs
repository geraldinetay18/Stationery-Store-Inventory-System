/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;
using System;

namespace ADProject_Team10_WebApi.Services
{
    public interface IRetrievalDetailService
    {
        RetrievalDetail FindRetrievalDetail(int id);
        RetrievalDetail FindRetrievalDetail(string itemCode, int retrievalId);
        List<RetrievalDetail> FindAllRetrievalDetails();
        List<RetrievalDetail> FindRetrievalDetailsByItemCode(String itemCode);
        List<RetrievalDetail> FindRetrievalDetailsByRetrievalId(int id);
        //need a method for breakdown by Dept - do in RequisitionService
        int AddRetrievalDetail(RetrievalDetail retrievalDetail);
        int UpdateRetrievalDetail(RetrievalDetail retrievalDetail);
        int RemoveRetrievalDetail(int id);
    }
}