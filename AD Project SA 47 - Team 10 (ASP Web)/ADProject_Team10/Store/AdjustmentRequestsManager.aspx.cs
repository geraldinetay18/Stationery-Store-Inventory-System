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
    public partial class AdjustmentRequestsManager : System.Web.UI.Page
    {
        StockAdjustmentBizLogic saLogic = new StockAdjustmentBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Manager"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    lblSupervisor.Text = saLogic.FindSupervisorName();
                    RefreshPage();
                }
            }
        }

        protected string FindDescription(string itemCode)
        {
            return saLogic.FindDescription(itemCode);
        }

        protected double FindAdjustmentCost(string itemCode, int quantity)
        {
            return saLogic.FindAdjustmentCost(itemCode, quantity);
        }

        protected int FindCurrentQuantity(string itemCode)
        {
            return saLogic.FindCurrentQuantityByItemCode(itemCode);
        }

        protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvItems.Rows)
            {
                ((CheckBox)row.FindControl("chkSelect")).Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            // Modify HEADER checkbox according to mini boxes

            CheckBox chkSelectHeader = (CheckBox)gvItems.HeaderRow.FindControl("chkSelectHeader");

            // 1. CHECKED HEADER
            if (chkSelectHeader.Checked)
                chkSelectHeader.Checked = ((CheckBox)sender).Checked;

            // 2. UNCHECKED HEADER
            else
            {
                bool allCheckBoxesChecked = true;
                foreach (GridViewRow row in gvItems.Rows)
                {
                    if (!((CheckBox)row.FindControl("chkSelect")).Checked)
                    {
                        allCheckBoxesChecked = false;
                        break;
                    }
                }
                chkSelectHeader.Checked = allCheckBoxesChecked;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            List<int> listAdjustmentId = GetSelectedAdjustmentId();

            // At least one adjustment selected
            if (listAdjustmentId.Count > 0)
            {
                saLogic.ApproveAdjustmentList(listAdjustmentId, txbRemarks.Text, (int)Session["employeeId"]);
                RefreshPage();
                Response.Write("<script>alert('Successful Approval'); </script>");
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            List<int> listAdjustmentId = GetSelectedAdjustmentId();

            // At least one adjustment selected
            if (listAdjustmentId.Count > 0)
            {
                saLogic.RejectAdjustmentList(listAdjustmentId, txbRemarks.Text, (int)Session["employeeId"]);
                RefreshPage();
                Response.Write("<script>alert('Successful Rejection'); </script>");
            }
        }

        List<int> GetSelectedAdjustmentId()
        {
            List<int> listAdjustmentId = new List<int>(); // For appending selected adjustments

            // Find id of selected adjustments
            foreach (GridViewRow row in gvItems.Rows)
            {
                if (((CheckBox)row.FindControl("chkSelect")).Checked)
                    listAdjustmentId.Add(Convert.ToInt32(((Label)row.FindControl("lblAdjustmentId")).Text));
            }
            return listAdjustmentId;
        }

        void RefreshPage()
        {
            List<StockAdjustment> listAdjustments = saLogic.FindAllReported();
            gvItems.DataSource = listAdjustments;
            gvItems.DataBind();
            txbRemarks.Text = "";

            if (listAdjustments.Count > 0)
            {
                PanelMessage.Visible = false;
                PanelNotes.Visible = true;
                PanelActions.Visible = true;
            }
            else
            {
                PanelMessage.Visible = true;
                PanelNotes.Visible = false;
                PanelActions.Visible = false;
            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get adjusted and current quantity
                int qtyAdjusted = Convert.ToInt32(((Label)e.Row.FindControl("lblQtyAdjusted")).Text);
                int qtyCurrent = Convert.ToInt32(((Label)e.Row.FindControl("lblQtyCurrent")).Text);

                if ((qtyCurrent + qtyAdjusted) < 0)
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}