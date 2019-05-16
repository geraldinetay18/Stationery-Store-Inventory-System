/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;

namespace ADProject_Team10.Store
{
    public partial class RequisitionsList : System.Web.UI.Page
    {
        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();
        int storeClerkId;
        List<Department> deptList;
        List<Requisition> reqList;
        static List<Requisition> approvedReqList;
        List<RequisitionDetail> reqDetList = new List<RequisitionDetail>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                storeClerkId = (int)Session["employeeId"];
                deptList = myBz.FindDepartmentByStoreClerkId(storeClerkId);
                reqList = new List<Requisition>();
                approvedReqList = new List<Requisition>();
                btnAddToRetrial.Attributes.Add("onclick", "return confirm('Are you sure that you would like to create new Retrieval Form?');");

                foreach (Department dp in deptList)
                {
                    approvedReqList.AddRange(myBz.FindAllReqByDeptIdAndStatus(dp.DeptId, "Approved"));
                    reqList.AddRange(myBz.FindAllReqByDeptIdAndStatus(dp.DeptId, "Approved"));
                    reqList.AddRange(myBz.FindAllReqByDeptIdAndStatus(dp.DeptId, "In Progress"));
                    reqList.AddRange(myBz.FindAllReqByDeptIdAndStatus(dp.DeptId, "Completed"));
                }
                approvedReqList.Sort((x, y) => x.RequisitionDate.CompareTo(y.RequisitionDate));
                reqList.Sort((x, y) => y.RequisitionDate.CompareTo(x.RequisitionDate));

                if (!IsPostBack)
                {
                    BindDDL();
                    BindGrid();
                    if (approvedReqList.Count == 0)
                    {
                        btnAddToRetrial.Visible = false;
                        if (HaveExistingRetrieval())
                        {
                            btnMoveToRetrieval.Visible = true;
                            lblHadRetrieval.Text = "There is outstanding retrieval, please complete it to be able to Create New Retrieval Form.";
                            lblHadRetrieval.ForeColor = System.Drawing.Color.Red;
                            lblHadRetrieval.Visible = true;
                        }
                        else
                            btnMoveToRetrieval.Visible = false;
                    }
                    else
                    {
                        if (HaveExistingRetrieval())
                        {
                            btnAddToRetrial.Visible = false;
                            btnMoveToRetrieval.Visible = true;
                            lblHadRetrieval.Text = "There is outstanding retrieval, please complete it to be able to Create New Retrieval Form.";
                            lblHadRetrieval.ForeColor = System.Drawing.Color.Red;
                            lblHadRetrieval.Visible = true;
                        }
                    }
                }
            }
        }
        protected void BindDDL()
        {
            //Department
            Department all = new Department();
            all.DeptId = "ALL";
            all.DeptName = "ALL";

            List<Department> deptListForDDL = new List<Department>();
            deptListForDDL.Add(all);
            deptListForDDL.AddRange(deptList);
            foreach (Department d in deptListForDDL.ToList())
            {
                if (d.DeptId == "STOR")
                {
                    deptListForDDL.Remove(d);
                }
            }
            ddlDept.DataSource = deptListForDDL;
            ddlDept.DataValueField = "DeptId";
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataBind();
        }
        public void BindGrid()
        {
            lblNotification.Text = approvedReqList.Count.ToString() + " approved requisition(s) waiting for retrieval";
            gvDeptReqList.DataSource = reqList;
            gvDeptReqList.DataBind();
        }

        protected string GetDeptNameByReqId(int reqId)
        {
            Department dp = myBz.FindDeptByReqId(reqId);
            return dp.DeptName;
        }

        protected void btnAddToRetrial_Click(object sender, EventArgs e)
        {
            AddNewRetrieval();
            int retrId = myBz.FindRetrListByEmplIdAndStatus(storeClerkId, "In Progress").FirstOrDefault().RetrievalId;

            //Get all RequisitionDetails
            foreach (Requisition r in approvedReqList)
            {
                myBz.UpdateReqStatus(r.RequisitionId, "In Progress");
                reqDetList.AddRange(myBz.FindAllReqDetByReqId(r.RequisitionId));
            }

            //Use RequisitionDetails to Create Retrievals and Disbursement:
            //For Retrieval <----------Dictionary<ItemCode,TotalRequestedQty> 
            Dictionary<string, int> retrItemQty = new Dictionary<string, int>();
            //For Disbursement <----------Dictionary<DepartmentId, Dictionary<ItemCode,TotalRequestedQty>>
            Dictionary<string, Dictionary<String, int>> deptDisbList = new Dictionary<string, Dictionary<String, int>>();

            foreach (RequisitionDetail reqDet in reqDetList)
            {
                //For retrieval
                if (!retrItemQty.Keys.Contains(reqDet.ItemCode))
                    retrItemQty.Add(reqDet.ItemCode, reqDet.QuantityRequest);
                else
                    retrItemQty[reqDet.ItemCode] += reqDet.QuantityRequest;

                //For Disbursement
                string deptId = myBz.FindDeptByReqId(reqDet.RequisitionId).DeptId;
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
                if (!HaveExistingDisbursement(entry.Key))
                {
                    AddDisbursement(entry.Key);
                }

                Disbursement currentDisb = myBz.FindDisbursementsByStatus("Pending").Where(x => x.StoreClerkId == storeClerkId && x.DeptId == entry.Key).First();
                if (currentDisb.Status == "Pending")
                {
                    foreach (KeyValuePair<string, int> disItemQty in entry.Value)
                    {
                        DisbursementDetail disbDet = new DisbursementDetail();
                        disbDet.ItemCode = disItemQty.Key;
                        disbDet.DisbursementId = currentDisb.DisbursementId;
                        disbDet.QuantityRequested = disItemQty.Value;
                        Stationery s = myBz.FindStationeryByItemCode(disItemQty.Key);
                        if (disItemQty.Value <= s.QuantityInStock)
                            disbDet.QuantityReceived = disItemQty.Value;
                        else
                            disbDet.QuantityReceived = s.QuantityInStock;
                        myBz.AddDisbDetail(disbDet);
                    }
                    currentDisb.Status = "In Progress";
                    myBz.UpdateDisbursement(currentDisb);
                }
            }

            //Sorting retdet by location
            var newRetrDetList = retrItemQty.Keys.ToList();
            newRetrDetList.Sort((x, y) => myBz.FindStationeryByItemCode(x).Bin.CompareTo(myBz.FindStationeryByItemCode(y).Bin));
            //Create Retrieval
            foreach (var key in newRetrDetList)
            {
                Stationery s = myBz.FindStationeryByItemCode(key);
                if (retrItemQty[key] <= s.QuantityInStock)
                    myBz.AddNewRetrievalDetail(retrId, key, retrItemQty[key], retrItemQty[key]);
                else
                    myBz.AddNewRetrievalDetail(retrId, key, retrItemQty[key], s.QuantityInStock);
            }

            //Redirect to retrieval form
            Response.Redirect("~/Store/RetrievalNew.aspx");
        }

        protected int AddNewRetrieval()
        {
            Retrieval retr = new Retrieval();
            retr.EmployeeId = storeClerkId;
            retr.DateRetrieved = DateTime.Today;
            retr.Status = "In Progress";
            return myBz.AddNewRetrieval(retr);
        }
        protected int AddDisbursement(string deptId)
        {
            Disbursement disb = new Disbursement();
            disb.DeptId = deptId;
            disb.StoreClerkId = storeClerkId;
            disb.RepresentativeId = myBz.FindDeptByDeptId(deptId).RepresentativeId;
            disb.DateCreated = DateTime.Now;
            disb.Status = "Pending";
            return myBz.AddDisbursement(disb);
        }

        //Check Existing Retrieval & Disbursement
        public bool HaveExistingRetrieval()
        {
            List<Retrieval> retrList = myBz.FindRetrListByEmplIdAndStatus(storeClerkId, "In Progress");
            return retrList.Count != 0 ? true : false;
        }
        public bool HaveExistingDisbursement(string deptId)
        {
            List<Disbursement> disbList = myBz.FindDisbursementsByEmplIdDeptStatus(storeClerkId, deptId, "Pending");
            return disbList.Count != 0 ? true : false;
        }

        //Buttons Control
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            List<Requisition> reqListGenerate = new List<Requisition>();
            if (ddlDept.SelectedValue == "ALL")
            {
                if (ddlStatus.SelectedValue == "ALL")
                    reqListGenerate = checkDate(reqList);
                else
                {
                    List<Requisition> statusReqList = myBz.FilterReqsByStatus(reqList, ddlStatus.SelectedValue);
                    reqListGenerate = checkDate(statusReqList);
                }
            }
            else
            {
                List<Requisition> deptReqList = myBz.FilterReqsByDeptId(reqList, ddlDept.SelectedValue);
                if (ddlStatus.SelectedValue == "ALL")
                {
                    reqListGenerate = checkDate(deptReqList);
                }
                else
                {
                    List<Requisition> deptStatusReqList = myBz.FilterReqsByStatus(deptReqList, ddlStatus.SelectedValue);
                    reqListGenerate = checkDate(deptStatusReqList);
                }
            }


            gvDeptReqList.DataSource = reqListGenerate;
            gvDeptReqList.DataBind();
        }
        protected List<Requisition> checkDate(List<Requisition> reqs)
        {
            List<Requisition> reqListGenerate = new List<Requisition>();
            if (tbFrom.Text != "" && tbFrom.Text != null)
            {
                if (tbTo.Text != "" && tbTo.Text != null)
                    reqListGenerate = myBz.FilterReqsByTo(myBz.FilterReqsByFrom(reqs, DateTime.Parse(tbFrom.Text)), DateTime.Parse(tbTo.Text));
                else
                    reqListGenerate = myBz.FilterReqsByFrom(reqs, DateTime.Parse(tbFrom.Text));
            }
            else
            {
                if (tbTo.Text != "" && tbTo.Text != null)
                    reqListGenerate = myBz.FilterReqsByTo(reqs, DateTime.Parse(tbFrom.Text));
                else
                    reqListGenerate = reqs;
            }
            return reqListGenerate;
        }

        protected bool IsBelongToDeptList(Requisition r)
        {
            Employee e = myBz.FindEmpById(r.EmployeeId);
            Department d = myBz.FindDeptByDeptId(e.DeptId);
            foreach (Department dp in deptList)
            {
                if (dp.DeptId == d.DeptId)
                    return true;
            }
            return false;
        }

        protected void btnMoveToRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/RetrievalNew.aspx");
        }

        protected void btnListAll_Click(object sender, EventArgs e)//to check again after do generate
        {
            Response.Redirect("~/Store/RequisitionsList.aspx");
        }

        protected void gvDeptReqList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string s;
                switch (s = (string)DataBinder.Eval(e.Row.DataItem, "RequisitionStatus"))
                {
                    case "Approved": e.Row.Cells[4].ForeColor = System.Drawing.Color.Green; break;
                    case "In Progress":
                    case "Allocating": e.Row.Cells[4].ForeColor = System.Drawing.Color.Orange; break;
                    case "Completed": e.Row.Cells[4].ForeColor = System.Drawing.Color.Red; break;
                    default: e.Row.Cells[4].ForeColor = System.Drawing.Color.Gray; break;
                }
            }
        }
    }
}