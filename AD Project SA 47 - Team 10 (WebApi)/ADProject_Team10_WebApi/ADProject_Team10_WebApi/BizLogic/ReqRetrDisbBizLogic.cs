/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.Models;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using ADProject_Team10_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADProject_Team10_WebApi.BizLogic
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

        StockAdjustmentBizLogic stkAdjBz = new StockAdjustmentBizLogic();
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();

        //Requisition
        public List<Requisition> FindAllReqByDeptIdAndStatus(string deptId, string status)//////////////////////////for ReqList
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
        public Requisition FindReqById(int reqId)///////////////////////////////////////for ReqDet
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
        public List<RequisitionDetail> FindAllReqDetByReqId(int reqId) /////////////////////////////////////////for ReqDet
        {
            return reqDetailServ.SearchRequisitionDetailByRequisitionId(reqId);
        }
        public List<RequisitionDetail> FindAllReqDetByClerkDeptAndStatus(string deptId, string status)
        {
            List<Requisition> deptReqList = FindAllReqByDeptIdAndStatus(deptId, status);

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
        public List<RetrievalDetail> FindRetrDetByRetrId(int retrId)/////////////////////////////for retrieval -- 2
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
        public Retrieval FindRetrByEmplIdAndStatus(int empid, string status)////////////////////////for ReqList  --- 1
        {
            return retrServ.FindRetrievalsByEmpIdAndStatus(empid, status).FirstOrDefault(); //////////////////
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
            return retrList.Where(x => x.Status == status).ToList();
        }
        public List<string> FindRetrievalStatusList()
        {
            return retrServ.FindRetrievalStatusList();
        }

        //Disbursement
        public int AddDisbursement(Disbursement disbursement)
        {
            return disbService.AddDisbursement(disbursement);
        }
        public int UpdateDisbursement(Disbursement disbursement)
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
        public List<Disbursement> FindDisbursementsByEmplIdDeptStatus(int storeClerkId, string deptId, string status)
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
            foreach (Department d in deptList)
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
        public List<DisbursementDetail> FindDisbDetsForNestedList(int storeClerkId, string status, string itemCode)
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
            return disbList.Where(x => x.DateDisbursed >= from || x.DateDisbursed == null).ToList();
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
            foreach (Department d in deptList)
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


        //others
        public int UpdateStationery(Stationery stat)
        {
            return statServ.UpdateStationery(stat);
        }
        public Stationery FindStationeryByItemCode(string itemCode)/////////////////////////////////////////////for ReqDet,retr  -- 3
        {
            return statServ.FindStationeryById(itemCode);
        }
        public Employee FindEmpByReqId(int reqId)///////////////////////////////////////////////////////////////for ReqDet
        {
            Requisition r = reqServ.SearchRequisitionbyID(reqId);
            return empServ.SearchEmployeeByEmpId(r.EmployeeId);
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
        public List<Department> FindDepartmentByCollectionStoreClerkId(int id) //////////////////////////////////for ReqList
        {
            List<CollectionPoint> cpList = cpServ.GetCollectionPointsByEmpId(id);
            List<Department> deptList = new List<Department>();
            foreach (CollectionPoint cp in cpList)
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
            Employee e = empServ.SearchEmployeeByEmpId(empId);
            return deptServ.SearchDepartmentByDeptId(e.DeptId);
        }
        public Department FindDeptByReqId(int reqId)/////////////////////////////////////////////////////for ReqDet
        {
            Requisition req = reqServ.SearchRequisitionbyID(reqId);
            return FindDeptByEmpId((int)req.ApprovedByEmployeeId);
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
        public StockManagement FindStockManagementBySourceId(string source, int id)
        {
            return stServ.FindStockManagementBySourceId(source, id);
        }
        public int UpdateStockManagement(StockManagement sm)
        {
            return stServ.UpdateStockManagement(sm);
        }


        //webAPI for Requisition List
        public int CreateDisbursementList(List<Requisition> approvedReqList, int storeClerkId)
        {
            try
            {
                //Get all RequisitionDetails
                List<RequisitionDetail> reqDetList = GetReqDetsByListReqs(approvedReqList);

                //Use RequisitionDetails to Create Retrievals and Disbursement:
                //For Disbursement <----------Dictionary<DepartmentId, Dictionary<ItemCode,TotalRequestedQty>>
                Dictionary<string, Dictionary<String, int>> deptDisbList = new Dictionary<string, Dictionary<String, int>>();

                foreach (RequisitionDetail reqDet in reqDetList)
                {
                    string deptId = FindDeptByReqId(reqDet.RequisitionId).DeptId;
                    if (!deptDisbList.Keys.Contains(deptId))
                    {
                        Dictionary<string, int> a = new Dictionary<string, int>();
                        a.Add(reqDet.ItemCode, reqDet.QuantityRequest);
                        deptDisbList.Add(deptId, a);
                    }
                    else
                    {
                        if (!deptDisbList[deptId].Keys.Contains(reqDet.ItemCode))
                            deptDisbList[deptId].Add(reqDet.ItemCode, reqDet.QuantityRequest);
                        else
                            deptDisbList[deptId][reqDet.ItemCode] += reqDet.QuantityRequest;
                    }
                }

                //Create Disbursement for each Dept and DisbursementDetails
                foreach (KeyValuePair<string, Dictionary<string, int>> entry in deptDisbList)
                {
                    if (!HaveExistingDisbursement(storeClerkId, entry.Key))
                    {
                        Disbursement disb = new Disbursement();
                        disb.DeptId = entry.Key;
                        disb.StoreClerkId = storeClerkId;
                        disb.RepresentativeId = FindDeptByDeptId(entry.Key).RepresentativeId;
                        disb.DateCreated = DateTime.Now;
                        disb.Status = "Pending";
                        return AddDisbursement(disb);
                    }

                    Disbursement currentDisb = FindDisbursementsByStatus("Pending").Where(x => x.StoreClerkId == storeClerkId && x.DeptId == entry.Key).First();

                    foreach (KeyValuePair<string, int> disItemQty in entry.Value)
                    {
                        DisbursementDetail disbDet = new DisbursementDetail();
                        disbDet.ItemCode = disItemQty.Key;
                        disbDet.DisbursementId = currentDisb.DisbursementId;
                        disbDet.QuantityRequested = disItemQty.Value;
                        Stationery s = FindStationeryByItemCode(disItemQty.Key);
                        if (disItemQty.Value <= s.QuantityInStock)
                            disbDet.QuantityReceived = disItemQty.Value;
                        else
                            disbDet.QuantityReceived = s.QuantityInStock;
                        AddDisbDetail(disbDet);
                    }
                    currentDisb.Status = "In Progress";
                    UpdateDisbursement(currentDisb);
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int CreateRetrievalDetail(List<Requisition> approvedReqList, int storeClerkId)
        {
            try
            {
                int retrId = FindRetrByEmplIdAndStatus(storeClerkId, "In Progress").RetrievalId;

                //Get all RequisitionDetails
                List<RequisitionDetail> reqDetList = GetReqDetsByListReqs(approvedReqList);

                //Use RequisitionDetails to Create Retrievals and Disbursement:
                //For Retrieval <----------Dictionary<ItemCode,TotalRequestedQty> 
                Dictionary<string, int> retrItemQty = new Dictionary<string, int>();

                foreach (RequisitionDetail reqDet in reqDetList)
                {
                    //for retrieval
                    if (!retrItemQty.Keys.Contains(reqDet.ItemCode))
                        retrItemQty.Add(reqDet.ItemCode, reqDet.QuantityRequest);
                    else
                        retrItemQty[reqDet.ItemCode] += reqDet.QuantityRequest;
                }

                //sorting retdet by location
                var newRetrDetList = retrItemQty.Keys.ToList();
                newRetrDetList.Sort((x, y) => FindStationeryByItemCode(x).Bin.CompareTo(FindStationeryByItemCode(y).Bin));
                //Create Retrieval
                foreach (var key in newRetrDetList)
                {
                    Stationery s = FindStationeryByItemCode(key);
                    if (retrItemQty[key] <= s.QuantityInStock)
                        AddNewRetrievalDetail(retrId, key, retrItemQty[key], retrItemQty[key]);
                    else
                        AddNewRetrievalDetail(retrId, key, retrItemQty[key], s.QuantityInStock);
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<RequisitionDetail> GetReqDetsByListReqs(List<Requisition> reqList)
        {
            List<RequisitionDetail> reqDetList = new List<RequisitionDetail>();
            foreach (Requisition r in reqList)
            {
                reqDetList.AddRange(FindAllReqDetByReqId(r.RequisitionId));
            }
            return reqDetList;
        }

        public bool HaveExistingDisbursement(int storeClerkId, string deptId)
        {
            List<Disbursement> disbList = FindDisbursementsByEmplIdDeptStatus(storeClerkId, deptId, "Pending");
            return disbList.Count != 0 ? true : false;
        }

        //webAPI for Retrieval New
        public int ConfirmRetrieval(int storeClerkId, int retrId)
        {
            try
            {
                List<RetrievalDetail> retrDetList = FindRetrDetByRetrId(retrId);
                for (int i = 0; i < retrDetList.Count; i++)
                {
                    //Update Disbursement Detail after retrieval
                    List<DisbursementDetail> disbursementDetails = FindDisbDetsForNestedList(storeClerkId, "In Progress", retrDetList[i].ItemCode);
                    if (retrDetList[i].QuantityRetrieved < retrDetList[i].QuantityNeeded)
                    {
                        int totalDisbQty = (int)retrDetList[i].QuantityRetrieved;
                        foreach (DisbursementDetail dd in disbursementDetails)
                        {
                            if (totalDisbQty > 0)
                            {
                                if (totalDisbQty >= dd.QuantityRequested)
                                {
                                    totalDisbQty -= dd.QuantityRequested;
                                }
                                else
                                {
                                    dd.QuantityReceived = totalDisbQty;
                                    UpdateDisbursementDetail(dd);
                                    totalDisbQty = 0;
                                }
                            }
                            else
                            {
                                dd.QuantityReceived = 0;
                                UpdateDisbursementDetail(dd);
                            }
                        }
                    }

                    //update stock
                    Stationery stat = FindStationeryByItemCode(retrDetList[i].ItemCode);
                    stat.QuantityInStock -= (int)retrDetList[i].QuantityRetrieved;
                    UpdateStationery(stat);

                    //Add Stock management
                    foreach (DisbursementDetail dd in disbursementDetails)
                    {
                        if (dd.QuantityReceived != 0)
                        {
                            StockManagement tm = new StockManagement();
                            tm.Date = DateTime.Today;
                            tm.ItemCode = retrDetList[i].ItemCode;
                            tm.StoreClerkId = storeClerkId;
                            tm.Source = "DIS";
                            tm.SourceId = dd.DisbursementDetailsId;
                            tm.QtyAdjusted = -dd.QuantityReceived;
                            tm.Balance = stat.QuantityInStock;
                            if (AddNewStockManagement(tm) == 0)
                                return 0;
                        }
                    }
                }

                //Check & update Disbursement status
                List<Disbursement> disbursements = FindDisbursementsByStatus("In Progress").Where(x => x.StoreClerkId == storeClerkId).ToList();
                foreach (Disbursement d in disbursements)
                {
                    d.Status = "Allocating";
                    UpdateDisbursement(d);
                }

                //update Retrieval status
                Retrieval retrieval = FindRetrByEmplIdAndStatus(storeClerkId, "In Progress");
                retrieval.Status = "Allocating";
                UpdateRetrieval(retrieval);

                //update requisitions status
                List<Department> deptList = FindDepartmentByStoreClerkId(storeClerkId);
                List<Requisition> approvedReqList = new List<Requisition>();
                foreach (Department dp in deptList)
                {
                    List<Requisition> r = FindAllReqByDeptIdAndStatus(dp.DeptId, "In Progress");
                    if (r != null)
                        approvedReqList.AddRange(r);
                }
                approvedReqList.Sort((x, y) => x.RequisitionDate.CompareTo(y.RequisitionDate));
                foreach (Requisition r in approvedReqList)
                {
                    UpdateReqStatus(r.RequisitionId, "Completed");
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //for new Retrieval Detail  --- 4
        public int UpdateRetDetRaiseSAV(int retrId, string itemCode, int qtyRetrieved, int raiseSAVQty, string reason, int storeClerkId)
        {
            try
            {
                //update qty retrieved
                RetrievalDetail rd = FindRetrDetByRetrIdAndItemCode(itemCode, retrId);
                rd.QuantityRetrieved = qtyRetrieved;
                UpdateRetrDet(rd);

                if (raiseSAVQty != 0)
                {
                    //update RetDet status
                    rd.Remark = raiseSAVQty + " " + reason;
                    UpdateRetrDet(rd);

                    //raise SAV
                    StockAdjustment stkAdjustment = new StockAdjustment();
                    stkAdjustment.ItemCode = itemCode;
                    stkAdjustment.QuantityAdjusted = raiseSAVQty;
                    stkAdjustment.Reason = reason;
                    stkAdjustment.DateCreated = DateTime.Today;
                    stkAdjustment.ClerkEmployeeId = storeClerkId;
                    if (stkAdjBz.AddAdjustment(stkAdjustment, "In Progress") == 0)
                        return 0;
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<RetrievalItem> GetRetrievalItems(int empId)
        {
            List<RetrievalItem> rIList = null;
            Retrieval ret = FindRetrByEmplIdAndStatus(empId, "In Progress");
            if (ret != null)
            {
                List<RetrievalDetail> rDList = FindRetrDetByRetrId(ret.RetrievalId);
                if (rDList.Count > 0)
                {
                    rIList = new List<RetrievalItem>();
                    foreach (RetrievalDetail rd in rDList)
                    {
                        Stationery sn = FindStationeryByItemCode(rd.ItemCode);
                        RetrievalItem rI = new RetrievalItem(rd, sn);
                        rIList.Add(rI);
                    }
                }
            }
            return rIList;
        }

        //for disbursement
        //1
        public List<Disbursement> FindDisbursementsByCollecionStoreClerkIdSortedByStatus(int storeClerkId)
        {
            List<Disbursement> result = FindDisbursementsByCollecionStoreClerkId(storeClerkId);
            result.Sort((x, y) => x.Status.CompareTo(y.Status));
            return result;
        }

        public List<BriefDisbursement> GetBriefDisbursements(int storeClerkId)
        {
            List<BriefDisbursement> bDList = null;
            List<Disbursement> disbursements = FindDisbursementsByCollecionStoreClerkIdSortedByStatus(storeClerkId);
            if (disbursements.Count != 0)
            {
                bDList = new List<BriefDisbursement>();
                foreach (Disbursement dis in disbursements)
                {
                    Employee rep = authMgmt.SearchEmployeeByEmpId(dis.RepresentativeId);
                    Department department = deptServ.SearchDepartmentByDeptId(dis.DeptId);
                    CollectionPoint cp = cpServ.GetCollectionPointById(department.LocationId);
                    BriefDisbursement briefDisbursement = new BriefDisbursement(dis, rep, cp);
                    bDList.Add(briefDisbursement);
                }
            }
            return bDList;
        }

        public List<BriefDisbursementDetails> GetBriefDisbursementDetailsList(int disbursementId)
        {
            List<BriefDisbursementDetails> bDDetailsList = null;
            List<DisbursementDetail> disbursementDetails = FindDisbDetByDisbId(disbursementId);

            if (disbursementDetails.Count != 0)
            {
                bDDetailsList = new List<BriefDisbursementDetails>();
                foreach (DisbursementDetail dd in disbursementDetails)
                {
                    Stationery sn = FindStationeryByItemCode(dd.ItemCode);
                    BriefDisbursementDetails briefDisbursementDetails = new BriefDisbursementDetails(dd, sn);
                    bDDetailsList.Add(briefDisbursementDetails);
                }
            }
            return bDDetailsList;

        }

        //2
        public int MarkAsNotCollected(int storeClerkId, int currentDisbId)
        {
            try
            {
                Disbursement currentDisb = FindDisbById(currentDisbId);
                List<DisbursementDetail> ddList = FindDisbDetByDisbId(currentDisbId);

                //return items to stock
                foreach (DisbursementDetail dd in ddList)
                {
                    Stationery stat = FindStationeryByItemCode(dd.ItemCode);
                    stat.QuantityInStock += dd.QuantityReceived;
                    UpdateStationery(stat);
                    StockManagement smNew = new StockManagement();
                    smNew.Date = DateTime.Today;
                    smNew.ItemCode = dd.ItemCode;
                    smNew.StoreClerkId = storeClerkId;
                    smNew.Source = "DIS";
                    smNew.SourceId = dd.DisbursementDetailsId;
                    smNew.QtyAdjusted = dd.QuantityReceived;
                    smNew.Balance = stat.QuantityInStock;
                    smNew.Date = DateTime.Today;
                    AddNewStockManagement(smNew);

                    dd.QuantityReceived = 0;
                    UpdateDisbursementDetail(dd);
                }

                //update current disbursement status
                currentDisb.Status = "Not Collected";
                UpdateDisbursement(currentDisb);

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //3
        public int RequestForAcknowledgement(int storeClerkId, int currentDisbId)
        {
            try
            {
                Disbursement currentDisb = FindDisbById(currentDisbId);
                List<DisbursementDetail> ddList = FindDisbDetByDisbId(currentDisbId);
                Department currentDept = FindDeptByDeptId(currentDisb.DeptId);

                //create Req Details
                Dictionary<string, int> reqList = new Dictionary<string, int>();
                for (int i = 0; i < ddList.Count; i++)
                {
                    int qtyReq = ddList[i].QuantityRequested;
                    int qtyReceived = ddList[i].QuantityReceived;

                    if (qtyReceived < qtyReq)
                    {
                        reqList.Add(ddList[i].ItemCode, qtyReq - qtyReceived);
                    }
                }

                //create new requisition to store data for things not disbursed
                if (reqList.Count != 0)
                {
                    Requisition req = new Requisition();
                    req.EmployeeId = storeClerkId;
                    req.RequisitionDate = DateTime.Today;
                    req.RequisitionStatus = "Approved";
                    req.Remark = $"Additional Requisition due to not fullfilled Disbursement: {currentDisb.DisbursementId}";
                    req.ApprovedByEmployeeId = currentDept.HeadId;
                    AddRequisiton(req);

                    Requisition currentReq = FindLatestReq();
                    foreach (KeyValuePair<string, int> r in reqList)
                    {
                        RequisitionDetail rd = new RequisitionDetail();
                        rd.RequisitionId = currentReq.RequisitionId;
                        rd.ItemCode = r.Key;
                        rd.QuantityRequest = r.Value;
                        AddRequisitionDetail(rd);
                    }
                }

                //change disb status to "Waiting for Acknowledgement"
                currentDisb.Status = "Waiting for Acknowledgement";
                UpdateDisbursement(currentDisb);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //4
        public int SaveEachDisbursingDetail(int storeClerkId, int disbursementDetailId, int receivedQty, int SAVQty, string reason)
        {
            try
            {
                DisbursementDetail dd = FindDisbursementDetailById(disbursementDetailId);
                Stationery stat = FindStationeryByItemCode(dd.ItemCode);

                //check amount
                if (receivedQty - SAVQty != dd.QuantityReceived)
                {
                    //"Please make sure Quantity Received + Quantity to Raise SAV = Expected Received Quantity";
                    return 0;
                }

                if (receivedQty < dd.QuantityReceived)
                {
                    //add Stock Adjustment
                    if (SAVQty != 0)
                    {
                        StockAdjustment stkAdjustment = new StockAdjustment();
                        stkAdjustment.ItemCode = dd.ItemCode;
                        stkAdjustment.QuantityAdjusted = SAVQty;
                        stkAdjustment.Reason = reason;
                        stkAdjustment.DateCreated = DateTime.Today;
                        stkAdjustment.ClerkEmployeeId = storeClerkId;
                        stkAdjBz.AddAdjustment(stkAdjustment, "In Progress");
                    }

                    //update Stock
                    stat.QuantityInStock -= SAVQty;
                    UpdateStationery(stat);

                    //update Stock Management
                    StockManagement smNew = new StockManagement();
                    smNew.Date = DateTime.Today;
                    smNew.ItemCode = dd.ItemCode;
                    smNew.StoreClerkId = storeClerkId;
                    smNew.Source = "DIS";
                    smNew.SourceId = dd.DisbursementDetailsId;
                    smNew.QtyAdjusted = -SAVQty;
                    smNew.Balance = stat.QuantityInStock;
                    smNew.Date = DateTime.Today;
                    AddNewStockManagement(smNew);

                    //save disbDets
                    dd.QuantityReceived = receivedQty;
                    dd.Remark = SAVQty + " " + reason;
                    UpdateDisbursementDetail(dd);
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}