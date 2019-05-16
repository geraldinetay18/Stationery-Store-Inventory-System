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
    public partial class AllocationStationery : System.Web.UI.Page
    {
        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();
        StockAdjustmentBizLogic stkAdjBz = new StockAdjustmentBizLogic();
        EmailBizLogic emBz = new EmailBizLogic();

        int storeClerkId;
        Employee storeClerk;
        Retrieval retrieval;
        List<RetrievalDetail> retrDetList;
        List<StockAdjustment> stkAdjList;

        //Overall page
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                storeClerkId = (int)Session["employeeId"];
                storeClerk = myBz.FindEmpById(storeClerkId);
                stkAdjList = (List<StockAdjustment>)Session["StockAdjustment"];
                PanelMessage.Visible = false;

                retrieval = myBz.FindRetrListByEmplIdAndStatus(storeClerkId, "Allocating").FirstOrDefault();

                if (retrieval != null)
                {
                    int retrId = retrieval.RetrievalId;
                    retrDetList = myBz.FindRetrDetByRetrId(retrId);
                    retrDetList.Sort((x, y) => GetStatus(y.ItemCode).CompareTo(GetStatus(x.ItemCode)));
                    if (!IsPostBack)
                    {
                        gvAllocation.DataSource = retrDetList;
                        gvAllocation.DataBind();
                    }
                }
                else
                {
                    PanelMessage.Visible = true;
                    PanelAll.Visible = false;
                }
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //Update retrieval status
            retrieval.Status = "Completed";
            myBz.UpdateRetrieval(retrieval);

            //Check & update Disbursement status
            List<Disbursement> disbursements = myBz.FindDisbursementsByStatus("Allocating").Where(x => x.StoreClerkId == storeClerkId).ToList();
            foreach (Disbursement d in disbursements)
            {
                Department dep = myBz.FindDeptByDisbId(d.DisbursementId);
                Employee rep = myBz.FindEmpById(dep.RepresentativeId);
                string subject = $"Stationery Disbursement Confirmed";

                string body = "Your Department's stationery is ready for collection.\n" /*+ details +*/ + "\n For more details, please visit SSIS. Thank you.";
                emBz.SendEmail(rep, subject, body);

                d.Status = "Allocated";
                int dateOfWeek =(int) DateTime.Today.DayOfWeek;
                if (dateOfWeek != 0)
                    d.DateDisbursed = DateTime.Today.AddDays(8 - dateOfWeek);
                else
                    d.DateDisbursed = DateTime.Today.AddDays(1);
                myBz.UpdateDisbursement(d);
            }
            Response.Redirect("~/Store/DisbursementList.aspx");
        }

        //For gvAllocation
        protected Stationery GetStationeryByItemCode(string itemCode)
        {
            return myBz.FindStationeryByItemCode(itemCode);
        }
        protected string GetStatus(string itemCode)
        {
            RetrievalDetail retrDet = myBz.FindRetrDetByRetrIdAndItemCode(itemCode, retrieval.RetrievalId);
            if (retrDet.QuantityNeeded == retrDet.QuantityRetrieved)
                return "Fulfilled";
            else
                return "Unfulfilled";
        }
        protected string MyNewRow(string itemCode)
        {
            return string.Format(@"</td></tr><tr id='tr{0}' class='collapsed-row'><td colspan='100' style='padding:0px;margin:0px;'>", itemCode);
        }
        protected void gvAllocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string itemCode = gvAllocation.DataKeys[e.Row.RowIndex].Value.ToString();
                var gvDepts = (GridView)e.Row.FindControl("gvDepartments");
                Label lblRetrQty = (Label)e.Row.FindControl("lblRetrQty");
                int totalRetr = Int32.Parse(lblRetrQty.Text);

                List<DisbursementDetail> disbursementDetails = myBz.FindDisbDetsForNestedList(storeClerkId, "Allocating", itemCode);

                gvDepts.DataSource = disbursementDetails;
                gvDepts.DataBind();
            }
        }

        protected void gvDepartments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnChange = (Button)e.Row.FindControl("btnChangeDisbQty");
                Label lblReqQty = (Label)e.Row.FindControl("lblReqQty");
                Label lblDisbQty = (Label)e.Row.FindControl("lblbDisbQty");
                if (Int32.Parse(lblReqQty.Text) == Int32.Parse(lblDisbQty.Text))
                    btnChange.Visible = false;

                GridView gv = (GridView)e.Row.NamingContainer;
                List<DisbursementDetail> list = (List<DisbursementDetail>)gv.DataSource;
                if (list.Count == 1)
                    btnChange.Visible = false;
            }
        }
        //For gvDepartments
        protected Department GetDeptByDisbDetId(int id)
        {
            return myBz.FindDeptByDisbDetId(id);
        }

        protected void gvDepartments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            Button btnUpdate = (Button)row.FindControl("btnUpdateDisbQty");
            Button btnChange = (Button)row.FindControl("btnChangeDisbQty");
            Button btnCancel = (Button)row.FindControl("btnCancel");

            Label lblDisbQty = (Label)row.FindControl("lblbDisbQty");
            TextBox tbDisbQty = (TextBox)row.FindControl("tbDisbQty");
            HiddenField hDDId = (HiddenField)row.FindControl("hDDId");

            if (e.CommandName == "ChangeDisbQty")
            {
                btnChange.Visible = false;
                lblDisbQty.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                tbDisbQty.Visible = true;
            }
            else
            {
                if (e.CommandName == "UpdateDisbQty")
                {
                    DisbursementDetail dd = myBz.FindDisbursementDetailById(Int32.Parse(hDDId.Value));
                    Stationery currentStat = myBz.FindStationeryByItemCode(dd.ItemCode);
                    GridView currentGridview = (GridView)row.Parent.Parent;
                    GridViewRow rowParent = (GridViewRow)currentGridview.NamingContainer;

                    Label totalRetrievedQty = (Label)rowParent.FindControl("lblRetrQty");

                    if (Int32.Parse(tbDisbQty.Text) > Int32.Parse(totalRetrievedQty.Text))
                    {
                        lblNotification.Text = "Please make sure Updating Receiving Quantity is not more than Total Retrieved Quantity";
                        row.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        row.ForeColor = System.Drawing.Color.Black;
                        List<DisbursementDetail> currentList = myBz.FindDisbDetsForNestedList(storeClerkId, "Allocating", dd.ItemCode);
                        int amountChange = Int32.Parse(tbDisbQty.Text) - dd.QuantityReceived;

                        //Find CurrentLocation of dd
                        int currentDDLocation = 0;
                        for (int i = 0; i < currentList.Count; i++)
                        {
                            if (currentList[i].DisbursementDetailsId == Int32.Parse(hDDId.Value))
                            {
                                currentDDLocation = i;
                                break;
                            }
                        }
                        currentList[currentDDLocation].QuantityReceived = Int32.Parse(tbDisbQty.Text);
                        myBz.UpdateDisbursementDetail(currentList[currentDDLocation]);

                        if (currentDDLocation != currentList.Count - 1)// if not the last dd in currentList
                        {
                            for (int i = currentDDLocation + 1; i < currentList.Count; i++)
                            {
                                if (amountChange > 0)
                                {
                                    if (currentList[i].QuantityReceived >= amountChange)
                                    {
                                        currentList[i].QuantityReceived -= amountChange;
                                        amountChange = 0;
                                        myBz.UpdateDisbursementDetail(currentList[i]);
                                        break;
                                    }
                                    else
                                    {
                                        currentList[i].QuantityReceived = 0;
                                        amountChange -= currentList[i].QuantityReceived;
                                        myBz.UpdateDisbursementDetail(currentList[i]);
                                    }
                                }
                            }

                            if (amountChange > 0)
                            {
                                for (int i = currentDDLocation - 1; i >= 0; i--)
                                {
                                    if (currentList[i].QuantityReceived >= amountChange)
                                    {
                                        currentList[i].QuantityReceived -= amountChange;
                                        amountChange = 0;
                                        break;
                                    }
                                    else
                                    {
                                        currentList[i].QuantityReceived = 0;
                                        amountChange -= currentList[i].QuantityReceived;
                                    }
                                    myBz.UpdateDisbursementDetail(currentList[i]);
                                }
                            }
                        }
                        else
                        {
                            if (amountChange > 0)
                            {
                                for (int i = currentDDLocation - 1; i >= 0; i--)
                                {
                                    if (currentList[i].QuantityReceived >= amountChange)
                                    {
                                        currentList[i].QuantityReceived -= amountChange;
                                        amountChange = 0;
                                    }
                                    else
                                    {
                                        currentList[i].QuantityReceived = 0;
                                        amountChange -= currentList[i].QuantityReceived;
                                    }

                                    myBz.UpdateDisbursementDetail(currentList[i]);
                                }
                            }
                        }
                        currentGridview.DataSource = currentList;
                        currentGridview.DataBind();
                        lblDisbQty.Text = tbDisbQty.Text;
                        btnChange.Visible = true;
                        lblDisbQty.Visible = true;
                        btnUpdate.Visible = false;
                        btnCancel.Visible = false;
                        tbDisbQty.Visible = false;
                    }
                }
                else if (e.CommandName == "CancelUpdate")
                {
                    btnChange.Visible = true;
                    lblDisbQty.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    tbDisbQty.Visible = false;
                }
            }
        }
    }
}