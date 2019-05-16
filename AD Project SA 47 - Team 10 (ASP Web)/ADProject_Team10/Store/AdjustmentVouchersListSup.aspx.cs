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
    public partial class AdjustmentVouchersListSup : System.Web.UI.Page
    {
        static StockAdjustmentBizLogic saLogic = new StockAdjustmentBizLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Context.User.IsInRole("Store Supervisor") || Context.User.IsInRole("Store Manager")))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    List<StockAdjustment> listVouchers = saLogic.FindAllVouchers();

                    if (listVouchers.Count > 0)
                    {
                        gvVouchers.DataSource = listVouchers;
                        gvVouchers.DataBind();
                        gvVouchers_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        PanelMessage.Visible = true;
                        PanelVoucherDetails.Visible = false;
                    }
                }
            }
        }

        protected void gvVouchers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected voucher
            GridViewRow row = gvVouchers.SelectedRow;
            int voucherNumber = Convert.ToInt32(row.Cells[1].Text);

            // Display its adjusted items
            List<StockAdjustment> listDetails = saLogic.FindAllAdjustmentsOfVoucher(voucherNumber);
            gvItems.DataSource = listDetails;
            gvItems.DataBind();

            // Display other voucher details (header, footer)
            StockAdjustment first = listDetails.First();
            lblVoucherNumber.Text = voucherNumber.ToString();
            lblDateIssued.Text = string.Format("{0:dd MMM yyyy}", first.DateApproved);
            lblApprovedBy.Text = FindEmpNameById((int)listDetails.First().ApprovedByEmployeeId);
            txbRemarks.Text = first.ApproverRemarks;

            // Display Total Adjustment amount of all details in this voucher
            lblTotalCost.Text = String.Format("{0:c}", saLogic.FindTotalCostOfVoucher(voucherNumber));
        }

        protected string FindEmpNameById(int id)
        {
            return saLogic.FindEmpNameById(id);
        }

        protected double FindTotalCost(int voucherNumber)
        {
            return saLogic.FindTotalCostOfVoucher(voucherNumber);
        }

        protected string FindDescription(string itemCode)
        {
            return saLogic.FindDescription(itemCode);
        }

        public double FindAdjustmentCost(string itemCode, int quantity)
        {
            return saLogic.FindAdjustmentCost(itemCode, quantity);
        }
    }
}