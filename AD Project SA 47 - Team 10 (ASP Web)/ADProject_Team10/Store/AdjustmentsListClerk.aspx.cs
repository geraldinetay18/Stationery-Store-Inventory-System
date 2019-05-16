/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    using BizLogic;
    using Services;
    using Models;
    public partial class AdjustmentsListClerk : System.Web.UI.Page
    {
        StockAdjustmentBizLogic saLogic = new StockAdjustmentBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    List<StockAdjustment> listAdjustments = saLogic.FindAllAdjustmentsOfClerkWithFilter((int)Session["employeeId"], "All");

                    if (listAdjustments.Count > 0)
                    {
                        gvItems.DataSource = listAdjustments;
                        gvItems.DataBind();
                    }
                    else
                    {
                        PanelMessage.Visible = true;
                        PanelDropdown.Visible = false;
                    }
                }
            }
        }

        protected string FindDescription(string itemCode)
        {
            return saLogic.FindDescription(itemCode);
        }

        public double FindAdjustmentCost(string itemCode, int quantity)
        {
            return saLogic.FindAdjustmentCost(itemCode, quantity);
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvItems.DataSource = saLogic.FindAllAdjustmentsOfClerkWithFilter(10032, ddlStatus.SelectedValue);
            gvItems.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/AdjustmentCreateClerk.aspx");
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Color-code status & allow edit option for "In Progress"
                string status = (string)DataBinder.Eval(e.Row.DataItem, "Status");
                switch (status)
                {
                    case "Reported":
                    case "Pending": e.Row.Cells[7].ForeColor = System.Drawing.Color.Orange; break;
                    case "Approved": e.Row.Cells[7].ForeColor = System.Drawing.Color.Green; break;
                    case "Rejected": e.Row.Cells[7].ForeColor = System.Drawing.Color.Red; break;
                    case "In Progress":
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
                        HyperLink link = (HyperLink)e.Row.FindControl("HyperLinkEdit");
                        link.NavigateUrl = "~/Store/AdjustmentCreateClerk?id=" + (int)DataBinder.Eval(e.Row.DataItem, "AdjustmentId");
                        link.Text = "Edit";
                        break;
                }

                // Manage null values for DateApproved
                if (DataBinder.Eval(e.Row.DataItem, "DateApproved") == null)
                    e.Row.Cells[5].Text = "None yet";

                if (DataBinder.Eval(e.Row.DataItem, "ApproverRemarks") == null || ((string)DataBinder.Eval(e.Row.DataItem, "ApproverRemarks")).Trim() == "")
                {
                    e.Row.Cells[6].Text = "NIL";
                }
            }
        }
    }
}