/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class RetrievalService : IRetrievalService
    {
        SSAEntities context = new SSAEntities();
        
        public Retrieval FindRetrieval(int id)
        {
            Retrieval retrieval = context.Retrievals.Where(x => x.RetrievalId == id).First();
            return retrieval;
        }
        public List<Retrieval> FindAllRetrievals()
        {
            List<Retrieval> retrievals = context.Retrievals.ToList();
            return retrievals;
        }
        public List<Retrieval> FindRetrievalsByDate(DateTime from, DateTime to)
        {
            List<Retrieval> retrievals = context.Retrievals.Where(x => x.DateRetrieved >= from && x.DateRetrieved <= to).ToList();
            return retrievals;
        }
        public List<Retrieval> FindRetrievalsByEmpId(int id)
        {
            List<Retrieval> retrievals = context.Retrievals.Where(x => x.EmployeeId == id).ToList();
            return retrievals;
        }

        public List<Retrieval> FindRetrievalsByEmpIdAndStatus(int empId, string status)
        {
            List<Retrieval> retrievals = context.Retrievals.Where(x => x.Status == status && x.EmployeeId==empId).ToList();
            return retrievals;
        }
        
        public int AddRetrieval(Retrieval retrieval)
        {
            context.Retrievals.Add(retrieval);
            return context.SaveChanges();
        }

        public int UpdateRetrieval(Retrieval retrieval)
        {
            Retrieval r = context.Retrievals.Where(x => x.RetrievalId == retrieval.RetrievalId).FirstOrDefault();
            r.Status = retrieval.Status;
            r.Remark = retrieval.Remark;
            return context.SaveChanges();
        }
        public List<string> FindRetrievalStatusList()
        {
            return context.Retrievals.Select(s => s.Status).Distinct().ToList();
        }
    }
}