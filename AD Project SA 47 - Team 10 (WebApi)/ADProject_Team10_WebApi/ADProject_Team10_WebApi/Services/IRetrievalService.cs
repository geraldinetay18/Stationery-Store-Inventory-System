/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;
using System;

namespace ADProject_Team10_WebApi.Services
{
    public interface IRetrievalService
    {
        Retrieval FindRetrieval(int id);
        List<Retrieval> FindAllRetrievals();
        List<Retrieval> FindRetrievalsByDate(DateTime from, DateTime to);
        List<Retrieval> FindRetrievalsByEmpId(int id);
        int AddRetrieval(Retrieval retrieval);
        List<Retrieval> FindRetrievalsByEmpIdAndStatus(int empId, string status);
        int UpdateRetrieval(Retrieval retrieval);
        List<string> FindRetrievalStatusList();
    }
}