/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10.Services;
using ADProject_Team10.Models;

namespace ADProject_Team10.BizLogic
{
    public class ReqRetrDisbBizLogic
    {
        IRequisitionService reqServ = new RequisitionService();
        IRequisitionDetailService reqDetailServ = new RequisitionDetailService();
        IRetrievalService retrServ = new RetrievalService();
        IRetrievalDetailService retrDetServ = new RetrievalDetailService();
        IDisbursementService disbService = new DisbursementService();
        IDisbursementDetailsService disbDetService = new DisbursementDetailsService();

        IEmployeeService empServ = new EmployeeService();
        IDepartmentService deptServ = new DepartmentService();
        IStationeryService statServ = new StationeryService();
        ICollectionPointService cpServ = new CollectionPointService();
        IStockManagementService stServ = new StockManagementService();

        //Requisition
        public List<Requisition> FindAllReqByDeptIdAndStatus(string deptId, string status)
        {
            List<Requisition> rList = new List<Requisition>();
            rList.AddRange(reqServ.SearchRequisitionbyStatus(status));

            List<Requisition> deptReqList = new List<Requisition>();
            foreach (Requisition r in rList)
            {
                if (FindDeptByReqId(r.RequisitionId).DeptId == deptId)
                {
                    deptReqList.Add(r);
                }
            }
            return deptReqList;
        }
        public List<Requisition> FindReqsByStoreClerkIdAndStatus(int storeClerkId, string status)
        {
            List<Requisition> reqList = new List<Requisition>();
            List<Department> depList = FindDepartmentByStoreClerkId(storeClerkId);
            foreach(Department d in depList)
            {
                reqList.AddRange(FindAllReqByDeptIdAndStatus(d.DeptId, status));
            }
            return reqList;
        }
        public Requisition FindReqById(int reqId)
        {
            return reqServ.SearchRequisitionbyID(reqId);
        }
        public Requisition FindLatestReq()
        {
            return reqServ.GetLastRequisitionId();
        }
        public int UpdateReqStatus(int reqId, string status)
        {
            return reqServ.UpdateReqStatus(reqId, status);
        }
        public List<Requisition> FindReqsByDate(DateTime from, DateTime to)
        {
            return reqServ.FindReqsByDate(from, to);
        }
        public int AddRequisiton(Requisition req)
        {
            return reqServ.CreateRequisition(req);
        }
        //for filter
        public List<Requisition> FilterReqsByFrom(List<Requisition> reqList, DateTime from)
        {
            return reqList.Where(x => x.RequisitionDate >= from || x.RequisitionDate == null).ToList();
        }
        public List<Requisition> FilterReqsByTo(List<Requisition> reqList, DateTime to)
        {
            return reqList.Where(x => x.RequisitionDate <= to).ToList();
        }
        public List<Requisition> FilterReqsByDeptId(List<Requisition> reqList, string deptId)
        {
            List<Requisition> result = new List<Requisition>();
            foreach (Requisition r in reqList)
            {
                Department dep = FindDeptByReqId(r.RequisitionId);
                if (dep.DeptId == deptId)
                    result.Add(r);
            }
            return result;
        }
        public List<Requisition> FilterReqsByStatus(List<Requisition> reqList, string status)
        {
            return reqList.Where(x => x.RequisitionStatus == status).ToList();
        }


        //Requisition Detail
        public List<RequisitionDetail> FindAllReqDetByReqId (int reqId)
        {
            return reqDetailServ.SearchRequisitionDetailByRequisitionId(reqId);
        }
        public List<RequisitionDetail> FindAllReqDetByClerkDeptAndStatus (string deptId,string status)
        {
            List<Requisition> deptReqList = FindAllReqByDeptIdAndStatus(deptId,status);

            List<RequisitionDetail> deptReqDetailList = new List<RequisitionDetail>();
            foreach (Requisition r in deptReqList)
            {
                deptReqDetailList.AddRange(reqDetailServ.SearchRequisitionDetailByRequisitionId(r.RequisitionId));
            }
            return deptReqDetailList;
        }
        public int AddRequisitionDetail(RequisitionDetail reqDet)
        {
            return reqDetailServ.CreateRequisitionDetail(reqDet);
        }

        //Retrieval Details
        public int AddNewRetrievalDetail(int retrId, string itemCode, int qtyNeeded, int qtyRetrieved)
        {
            RetrievalDetail retrDet = new RetrievalDetail();
            retrDet.RetrievalId = retrId;
            retrDet.ItemCode = itemCode;
            retrDet.QuantityNeeded = qtyNeeded;
            retrDet.QuantityRetrieved = qtyRetrieved;
            return retrDetServ.AddRetrievalDetail(retrDet);
        }
        public List<RetrievalDetail> FindRetrDetByRetrId(int retrId)
        {
            return retrDetServ.FindRetrievalDetailsByRetrievalId(retrId);
        }
        public RetrievalDetail FindRetrDetByRetrIdAndItemCode(string itemCode, int retrId)
        {
            return retrDetServ.FindRetrievalDetail(itemCode, retrId);
        }
        public int UpdateRetrDet(RetrievalDetail retrDet)
        {
            return retrDetServ.UpdateRetrievalDetail(retrDet);
        }

        //Retrieval
        public List<Retrieval> FindAllRetrievals()
        {
            return retrServ.FindAllRetrievals();
        }
        public int AddNewRetrieval(Retrieval retr)
        {
            return retrServ.AddRetrieval(retr);
        }
        public List<Retrieval> FindRetrListByEmplIdAndStatus(int empid, string status)
        {
            return retrServ.FindRetrievalsByEmpIdAndStatus(empid, status);
        }
        public int UpdateRetrieval(Retrieval retr)
        {
            return retrServ.UpdateRetrieval(retr);
        }
        //for filter
        public List<Retrieval> FilterRetrsByFrom(List<Retrieval> retrList, DateTime from)
        {
            return retrList.Where(x => x.DateRetrieved >= from || x.DateRetrieved == null).ToList();
        }
        public List<Retrieval> FilterRetrsByTo(List<Retrieval> retrList, DateTime to)
        {
            return retrList.Where(x => x.DateRetrieved <= to).ToList();
        }
        public List<Retrieval> FilterRetrsByStoreClerkId(List<Retrieval> retrList, int storeClerkId)
        {
            return retrList.Where(x => x.EmployeeId == storeClerkId).ToList();
        }
        public List<Retrieval> FilterRetrByStatus(List<Retrieval> retrList, string status)
        {
            return retrList.Where(x=>x.Status==status).ToList();
        }
        public List<string> FindRetrievalStatusList()
        {
            return new List<string> {"In Progress", "Completed" };
            //return retrServ.FindRetrievalStatusList();
        }

        //Disbursement
        public int AddDisbursement (Disbursement disbursement)
        {
            return disbService.AddDisbursement(disbursement);
        }
        public int UpdateDisbursement (Disbursement disbursement)
        {
            return disbService.UpdateDisbursement(disbursement);
        }
        public Disbursement FindDisbByStoreClerkIdStatusDeptId(int storeClerkId, string status, string deptId)
        {
            List<Disbursement> disbByStatus = this.FindDisbursementsByStatus(status);
            return disbByStatus.Where(x => x.StoreClerkId == storeClerkId && x.DeptId == deptId).FirstOrDefault();
        }
        public Disbursement FindDisbById(int id)
        {
            return disbService.GetDisbursementById(id);
        }
        public List<Disbursement> FindDisbursementsByStatus(string status)
        {
            return disbService.FindByStatus(status);
        }
        public List<Disbursement> FindDisbursementsByEmplIdDeptStatus(int storeClerkId,string deptId,string status)
        {
            return disbService.FindByStatus(status).Where(x => x.StoreClerkId == storeClerkId && x.DeptId == deptId).ToList();
        }
        public List<Disbursement> FindDisbursementsByDeptId(string deptId)
        {
            return disbService.GetDisbursementsByDeptId(deptId);
        }
        public List<Disbursement> FindDisbursementsByCollecionStoreClerkId(int empId)
        {
            List<Department> deptList = FindDepartmentByCollectionStoreClerkId(empId);
            List<Disbursement> disbList = new List<Disbursement>();
            foreach(Department d in deptList)
            {
                disbList.AddRange(FindDisbursementsByDeptId(d.DeptId));
            }
            return disbList;
        }

        //Disbursement Detail
        public int AddDisbDetail(DisbursementDetail disbursementDetail)
        {
            return disbDetService.AddDisbursementDetail(disbursementDetail);
        }
        public List<DisbursementDetail> FindDisbDetsForNestedList (int storeClerkId, string status, string itemCode)
        {
            return disbDetService.SearchDisbDetsForNestedList(storeClerkId, status, itemCode);
        }
        public List<DisbursementDetail> FindDisbDetByDisbId(int id)
        {
            return disbDetService.SearchDisbDetByDisbId(id);
        }

        //for filter
        public List<Disbursement> FilterDisbsByFrom(List<Disbursement> disbList, DateTime from)
        {
            return disbList.Where(x => x.DateDisbursed >= from ||x.DateDisbursed==null).ToList();
        }
        public List<Disbursement> FilterDisbsByTo(List<Disbursement> disbList, DateTime to)
        {
            return disbList.Where(x => x.DateDisbursed <= to).ToList();
        }
        public List<Disbursement> FilterDisbsByDeptId(List<Disbursement> disbList, string deptId)
        {
            return disbList.Where(x => x.DeptId == deptId).ToList();
        }
        public List<Disbursement> FilterDisbsByCollectionPointId(List<Disbursement> disbList, string cpId)
        {
            List<Department> deptList = FindDeptsByCollectionPointId(cpId);
            List<Disbursement> result = new List<Disbursement>();
            foreach(Department d in deptList)
            {
                result.AddRange(FilterDisbsByDeptId(disbList, d.DeptId));
            }
            return result;
        }
        public DisbursementDetail FindDisbursementDetailById(int id)
        {
            return disbDetService.SearchDisbursementDetailById(id);
        }
        public int UpdateDisbursementDetail(DisbursementDetail dd)
        {
            return disbDetService.UpdateDisbursementDetail(dd);
        }

        //Others
        public int UpdateStationery(Stationery stat)
        {
            return statServ.UpdateStationery(stat);
        }
        public Stationery FindStationeryByItemCode(string itemCode)
        {
            return statServ.FindStationeryById(itemCode);
        }
        public Employee FindEmpNameByReqId(int reqId)
        {
            return empServ.SearchEmployeeByReqId(reqId);
        }
        public Employee FindEmpById(int empId)
        {
            return empServ.SearchEmployeeByEmpId(empId);
        }
        public List<Employee> FindEmployeesByRole(string role)
        {
            return empServ.FindEmployeesByRole(role);
        }

        public List<Department> FindDepartmentByStoreClerkId(int id)
        {
            return deptServ.SearchDepartmentByStoreClerkEmpId(id);
        }
        public List<Department> FindDepartmentByCollectionStoreClerkId(int id)
        {
            List<CollectionPoint> cpList = cpServ.GetCollectionPointsByEmpId(id);
            List<Department> deptList = new List<Department>();
            foreach(CollectionPoint cp in cpList)
            {
                deptList.AddRange(FindDepartmentByCollectionPointId(cp.LocationId));
            }
            return deptList;
        }
        public List<Department> FindDepartmentByCollectionPointId(string id)
        {
            return deptServ.SearchDepartmentByLocationId(id);
        }
        public Department FindDeptByEmpId(int empId)
        {
            Employee e= empServ.SearchEmployeeByEmpId(empId);
            return deptServ.SearchDepartmentByDeptId(e.DeptId);
        }
        public Department FindDeptByReqId(int reqId)
        {
            Requisition req = reqServ.SearchRequisitionbyID(reqId);
            Employee e = empServ.SearchEmployeeByEmpId((int)req.ApprovedByEmployeeId);
            Department result = deptServ.SearchDepartmentByDeptId(e.DeptId);
            return result;
        }
        public Department FindDeptByDeptId(string deptId)
        {
            return deptServ.SearchDepartmentByDeptId(deptId);
        }
        public Department FindDeptByDisbDetId(int disbDetId)
        {
            DisbursementDetail disbDet = disbDetService.SearchDisbursementDetailById(disbDetId);
            return FindDeptByDisbId(disbDet.DisbursementId);
        }
        public Department FindDeptByDisbId(int disbId)
        {
            Disbursement d = disbService.GetDisbursementById(disbId);
            return deptServ.SearchDepartmentByDeptId(d.DeptId);
        }
        public List<Department> FindDeptsByCollectionPointId(string cpId)
        {
            CollectionPoint cp = cpServ.GetCollectionPointById(cpId);
            return deptServ.SearchDepartmentByLocationId(cp.LocationId);
        }
        public CollectionPoint FindCollectionPointByDeptId(string deptId)
        {
            Department dept = deptServ.SearchDepartmentByDeptId(deptId);
            return cpServ.GetCollectionPointById(dept.LocationId);
        }
        public List<CollectionPoint> FindCollectionPointByEmpId(int empId)
        {
            return cpServ.GetCollectionPointsByEmpId(empId);
        }
        public int AddNewStockManagement(StockManagement tm)
        {
            return stServ.AddStockManagement(tm);
        }
        public StockManagement FindStockManagementBySourceId(string source,int id)
        {
            return stServ.FindStockManagementBySourceId(source, id);
        }
        public int UpdateStockManagement(StockManagement sm)
        {
            return stServ.UpdateStockManagement(sm);
        }
    }
}