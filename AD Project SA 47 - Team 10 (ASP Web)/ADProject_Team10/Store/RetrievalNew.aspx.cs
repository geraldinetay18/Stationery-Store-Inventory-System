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
    public partial class RetrievalNew : System.Web.UI.Page
    {
        int storeClerkId;
        Retrieval retrieval;
        List<RetrievalDetail> retrDetList;
        int countRetrieved=0;

        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();
        StockAdjustmentBizLogic stkAdjBz = new StockAdjustmentBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                storeClerkId = (int)Session["employeeId"];

                retrieval = myBz.FindRetrListByEmplIdAndStatus(storeClerkId, "In Progress").FirstOrDefault();

                int retrId = retrieval.RetrievalId;
                lblRetrNum.Text = retrieval.RetrievalId.ToString();
                lblRetrDate.Text = string.Format("{0: dd MMM yyyy}", retrieval.DateRetrieved);
                retrDetList = myBz.FindRetrDetByRetrId(retrId);

                if (!IsPostBack)
                {
                    gvRetrList.DataSource = retrDetList;
                    gvRetrList.DataBind();
                }
            }
        }

        protected Stationery GetStationeryByItemCode(string itemCode)
        {
            return myBz.FindStationeryByItemCode(itemCode);
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < retrDetList.Count; i++)
            {
                TextBox tbRetrQty = (TextBox)gvRetrList.Rows[i].FindControl("tbRetrQty");
                CheckBox cbRetrieved = (CheckBox)gvRetrList.Rows[i].FindControl("cbRetrieved");
                TextBox tbRaiseSAV = (TextBox)gvRetrList.Rows[i].FindControl("tbRaiseSAV");
                TextBox tbReason = (TextBox)gvRetrList.Rows[i].FindControl("tbReason");
                Label lblReqQty = (Label)gvRetrList.Rows[i].FindControl("lblReqQty");
                Label lblStock =(Label)gvRetrList.Rows[i].FindControl("lblStock");

                if (tbRetrQty.Text != "0" && tbRetrQty.Text != null && tbRetrQty.Text != "")
                {
                    if (Int32.Parse(tbRetrQty.Text) > Int32.Parse(lblStock.Text))
                    {
                        tbRetrQty.BackColor = System.Drawing.Color.Red;
                        lblNotification.Text = "Please make sure you Retrieval Quantity is lesser or equal Stock Quantity";
                        return;
                    }
                    else
                    {
                        retrDetList[i].QuantityRetrieved = Int32.Parse(tbRetrQty.Text);
                    }
                }
                else
                {
                    retrDetList[i].QuantityRetrieved = 0;
                }

                retrDetList[i].Remark = tbRaiseSAV.Text + " " + tbReason.Text;
                myBz.UpdateRetrDet(retrDetList[i]);

                //update Stock
                Stationery stat = myBz.FindStationeryByItemCode(retrDetList[i].ItemCode);
                

                //Update Disbursement Detail after retrieval
                List<DisbursementDetail> disbursementDetails = myBz.FindDisbDetsForNestedList(storeClerkId, "In Progress", retrDetList[i].ItemCode);
                if(retrDetList[i].QuantityRetrieved< retrDetList[i].QuantityNeeded)
                {
                    int totalDisbQty=(int) retrDetList[i].QuantityRetrieved;
                    foreach(DisbursementDetail dd in disbursementDetails)
                    {
                        if (totalDisbQty > 0)
                        {
                            if (totalDisbQty >= dd.QuantityRequested)
                            {
                                totalDisbQty -= dd.QuantityRequested;
                            } else
                            {
                                dd.QuantityReceived = totalDisbQty;
                                myBz.UpdateDisbursementDetail(dd);
                                totalDisbQty = 0;
                            }
                        }
                        else
                        {
                            dd.QuantityReceived = 0;
                            myBz.UpdateDisbursementDetail(dd);
                        }
                    }
                }

                //Add Stock management
                foreach (DisbursementDetail dd in disbursementDetails)
                {
                    if (dd.QuantityReceived != 0)
                    {
                        stat.QuantityInStock -= dd.QuantityReceived;
                        myBz.UpdateStationery(stat);

                        StockManagement tm = new StockManagement();
                        tm.Date = DateTime.Today;
                        tm.ItemCode = retrDetList[i].ItemCode;
                        tm.StoreClerkId = storeClerkId;
                        tm.Source = "DIS";
                        tm.SourceId = dd.DisbursementDetailsId;
                        tm.QtyAdjusted = - dd.QuantityReceived;
                        tm.Balance = stat.QuantityInStock;
                        myBz.AddNewStockManagement(tm);
                    }
                }

                //for adjustment voucher
                if (tbRaiseSAV.Text != "0" && tbRaiseSAV.Text != null && tbRaiseSAV.Text != "")
                {

                    StockAdjustment stkAdjustment = new StockAdjustment();
                    stkAdjustment.ItemCode = retrDetList[i].ItemCode;
                    stkAdjustment.QuantityAdjusted = Int32.Parse(tbRaiseSAV.Text);
                    stkAdjustment.Reason = tbReason.Text;
                    stkAdjustment.DateCreated = DateTime.Today;
                    stkAdjustment.ClerkEmployeeId = storeClerkId;
                    stkAdjBz.AddAdjustment(stkAdjustment, "In Progress");
                }
            }

            //Check & update Disbursement status
            List<Disbursement> disbursements = myBz.FindDisbursementsByStatus("In Progress").Where(x => x.StoreClerkId == storeClerkId).ToList();
            foreach (Disbursement d in disbursements)
            {
                d.Status = "Allocating";
                myBz.UpdateDisbursement(d);
            }

            //update Retrieval status
            Retrieval retrieval = myBz.FindRetrListByEmplIdAndStatus(storeClerkId, "In Progress").FirstOrDefault();
            retrieval.Status = "Allocating";
            myBz.UpdateRetrieval(retrieval);

            //update requisitions status
            List<Requisition> reqList = myBz.FindReqsByStoreClerkIdAndStatus(storeClerkId, "In Progress");
            foreach(Requisition r in reqList)
            {
                myBz.UpdateReqStatus(r.RequisitionId, "Completed");
            }

            Session["Retrieval"] = retrieval;
            Response.Redirect("~/Store/AllocationStationery.aspx");
        }

        protected void btnTickAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < retrDetList.Count; i++)
            {
                CheckBox cbRetrieved = (CheckBox)gvRetrList.Rows[i].FindControl("cbRetrieved");
                cbRetrieved.Checked = true;
            }
        }
    }
}