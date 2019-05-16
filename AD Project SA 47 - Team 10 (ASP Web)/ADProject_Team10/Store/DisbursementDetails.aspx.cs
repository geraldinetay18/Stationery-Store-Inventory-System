/* Author: Tran Thi Ngoc Thuy (Saphira) */

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
    public partial class DisbursementDetails : System.Web.UI.Page
    {
        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();
        StockAdjustmentBizLogic stkAdjBz = new StockAdjustmentBizLogic();

        int storeClerkId;
        int disbID;
        Disbursement currentDisb;
        List<DisbursementDetail> ddList;
        Department currentDept;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    //to check location by store clerk => disbursement list
                    storeClerkId = (int)Session["employeeId"];

                    disbID = Int32.Parse(Request.QueryString["id"]);
                    currentDisb = myBz.FindDisbById(disbID);
                    currentDept = myBz.FindDeptByDeptId(currentDisb.DeptId);
                    ddList = myBz.FindDisbDetByDisbId(currentDisb.DisbursementId);

                    if (!IsPostBack)
                    {
                        BindData();
                    }

                    if (currentDisb.Status == "Allocated")
                        DisplayForAllocated(true);
                    else
                    {
                        DisplayForAllocated(false);
                    }
                }
                else
                    Response.Redirect("~/Store/DisbursementList.aspx");
            }
        }

        protected void DisplayForAllocated(bool a)
        {
            btnNotCollected.Visible = a;
            btnRequestAcknowledgement.Visible = a;
            gvDeptReqDetails.Visible = !a;
            gvUpdateReqDetails.Visible = a;
        }

        protected void BindData()
        {
            lblCollectionPoint.Text = myBz.FindCollectionPointByDeptId(currentDept.DeptId).LocationName;//to check with team if need to store this in Database as different time, different collection point of department
            lblDept.Text = currentDept.DeptName;
            lblDate.Text = currentDisb.DateDisbursed == null ? "None yet" : ((DateTime)currentDisb.DateDisbursed).ToShortDateString();
            lblTime.Text = myBz.FindCollectionPointByDeptId(currentDisb.DeptId).Time.ToString();
            lblRepName.Text = myBz.FindEmpById(currentDept.RepresentativeId).EmployeeName;
            lblStatus.Text = currentDisb.Status;
            lblStoreClerk.Text = currentDisb.Employee.EmployeeName;

            //Bind gridview
            gvDeptReqDetails.DataSource = ddList;
            gvDeptReqDetails.DataBind();
            gvUpdateReqDetails.DataSource = ddList;
            gvUpdateReqDetails.DataBind();
        }

        protected Stationery GetItem(string itemCode)
        {
            return myBz.FindStationeryByItemCode(itemCode);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/DisbursementList.aspx");
        }

        protected void btnRequestAcknowledgement_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ddList.Count; i++)
            {
                Label lblQtyReq = (Label)gvUpdateReqDetails.Rows[i].FindControl("lblQtyReq");
                TextBox tbQtyReceived = (TextBox)gvUpdateReqDetails.Rows[i].FindControl("tbQtyReceived");
                TextBox tbRaiseSAV = (TextBox)gvUpdateReqDetails.Rows[i].FindControl("tbRaiseSAV");
                TextBox tbReason = (TextBox)gvUpdateReqDetails.Rows[i].FindControl("tbReason");

                int qtyRec = (tbQtyReceived.Text != "") ? Int32.Parse(tbQtyReceived.Text) : 0;
                int qtyRaiseSAV = tbRaiseSAV.Text == null || tbRaiseSAV.Text == "" ? 0 : Int32.Parse(tbRaiseSAV.Text);

                Stationery stat = myBz.FindStationeryByItemCode(ddList[i].ItemCode);

                //check amount
                if (qtyRec - qtyRaiseSAV != ddList[i].QuantityReceived)
                {
                    lblNotification.Text = "Please make sure Quantity Received + Quantity to Raise SAV = Expected Received Quantity";
                    lblNotification.Visible = true;
                    return;
                }
                else
                {
                    lblNotification.Visible = false;
                }

                if (Int32.Parse(tbQtyReceived.Text) < ddList[i].QuantityReceived)
                {
                    //add Stock Adjustment
                    if (tbRaiseSAV.Text != "0" && tbRaiseSAV.Text != null && tbRaiseSAV.Text != "")
                    {
                        StockAdjustment stkAdjustment = new StockAdjustment();
                        stkAdjustment.ItemCode = ddList[i].ItemCode;
                        stkAdjustment.QuantityAdjusted = Int32.Parse(tbRaiseSAV.Text);
                        stkAdjustment.Reason = tbReason.Text;
                        stkAdjustment.DateCreated = DateTime.Today;
                        stkAdjustment.ClerkEmployeeId = storeClerkId;
                        stkAdjBz.AddAdjustment(stkAdjustment, "In Progress");
                    }

                    //update Stock
                    stat.QuantityInStock += Int32.Parse(tbRaiseSAV.Text);
                    myBz.UpdateStationery(stat);

                    //update Stock Management
                    StockManagement sm = new StockManagement();
                    sm.Date = DateTime.Today;
                    sm.ItemCode = ddList[i].ItemCode;
                    sm.StoreClerkId = storeClerkId;
                    sm.Source = "DIS";
                    sm.QtyAdjusted = -Int32.Parse(tbRaiseSAV.Text);
                    sm.Balance = stat.QuantityInStock;
                    sm.Date = DateTime.Today;
                    myBz.AddNewStockManagement(sm);

                    //save disbDets
                    ddList[i].QuantityReceived = Int32.Parse(tbQtyReceived.Text);
                    ddList[i].Remark = tbRaiseSAV.Text + " - " + tbReason.Text;
                    myBz.UpdateDisbursementDetail(ddList[i]);
                }
            }

            gvDeptReqDetails.DataSource = ddList;
            gvDeptReqDetails.DataBind();

            //create Req Details
            Dictionary<string, int> reqList = new Dictionary<string, int>();
            for (int i = 0; i < ddList.Count; i++)
            {
                Label lblQtyReq = (Label)gvDeptReqDetails.Rows[i].FindControl("lblQtyReq");
                Label lblQtyRec = (Label)gvDeptReqDetails.Rows[i].FindControl("lblQtyRec");
                Label lblRemark = (Label)gvDeptReqDetails.Rows[i].FindControl("lblRemark");
                int qtyReq = Int32.Parse(lblQtyReq.Text);
                int qtyReceived = Int32.Parse(lblQtyRec.Text);

                if (qtyReceived < qtyReq)
                {
                    reqList.Add(ddList[i].ItemCode, qtyReq - qtyReceived);
                }
            }

            if (reqList.Count != 0)
            {
                //create new requisition to store data for things not disbursed
                Requisition req = new Requisition();
                req.EmployeeId = storeClerkId;
                req.RequisitionDate = DateTime.Today;
                req.RequisitionStatus = "Approved";
                req.Remark = $"Additional Requisition due to not fullfilled Disbursement: {currentDisb.DisbursementId}";
                req.ApprovedByEmployeeId = currentDept.HeadId;
                myBz.AddRequisiton(req);

                Requisition currentReq = myBz.FindLatestReq();
                foreach (KeyValuePair<string, int> r in reqList)
                {
                    RequisitionDetail rd = new RequisitionDetail();
                    rd.RequisitionId = currentReq.RequisitionId;
                    rd.ItemCode = r.Key;
                    rd.QuantityRequest = r.Value;
                    myBz.AddRequisitionDetail(rd);
                }
            }

            //change disb status to "Waiting for Acknowledgement"
            currentDisb.Status = "Waiting for Acknowledgement";
            myBz.UpdateDisbursement(currentDisb);
            //onpage of representative: find:"Waiting for Acknowledgment", confirm:"Acknowledged", reject:"Allocated"

            BindData();
            DisplayForAllocated(false);
        }

        protected void btnNotCollected_Click(object sender, EventArgs e)
        {
            //return stock, update stock management, update DD
            foreach (DisbursementDetail dd in ddList)
            {
                Stationery stat = myBz.FindStationeryByItemCode(dd.ItemCode);
                stat.QuantityInStock += dd.QuantityReceived;
                myBz.UpdateStationery(stat);
                StockManagement sm = new StockManagement();
                sm.Date = DateTime.Today;
                sm.ItemCode = dd.ItemCode;
                sm.StoreClerkId = storeClerkId;
                sm.Source = "DIS";
                sm.QtyAdjusted = dd.QuantityReceived;
                sm.Balance = stat.QuantityInStock;
                myBz.AddNewStockManagement(sm);

                dd.QuantityReceived = 0;
                myBz.UpdateDisbursementDetail(dd);
            }

            //update disbursement status
            currentDisb.Status = "Not Collected";
            myBz.UpdateDisbursement(currentDisb);
            DisplayForAllocated(false);
            Response.Redirect("~/Store/DisbursementDetails.aspx");
        }
    }
}