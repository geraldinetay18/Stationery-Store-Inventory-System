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
    public partial class AdjustmentCreateClerk : System.Web.UI.Page
    {
        static StockAdjustmentBizLogic saLogic = new StockAdjustmentBizLogic();
        static string scriptSaveSuccess = "<script>alert('Success! Your adjustment has been saved.'); </script>";
        static string scriptSaveFail = "<script>alert('Oops, an error occurred. your adjustment was not saved.'); </script>";
        static string scriptSubmitSuccess = "<script>alert('Your adjustment has been succesffully submitted!'); </script>";
        static string scriptSubmitFail = "<script>alert('Oops, an error occurred. Submission unsuccessful.'); </script>";

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
                    // Populate controls
                    ddlCategory.DataSource = saLogic.FindAllCategories();
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();

                    PopulateCodeAndDescription();

                    // Directed from "Edit" of a particular adjustment or by URL
                    if (Request.QueryString["id"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        ViewState["id"] = id;
                        StockAdjustment adjustment = saLogic.FindAdjustmentById(id);

                        // Only display parsed in id if adjustment exists and is "In Progress"
                        if (adjustment != null && adjustment.Status == "In Progress")
                        {
                            // Display adjustment
                            ddlCategory.SelectedValue = adjustment.Stationery.CategoryId.ToString();
                            ddlCategory_SelectedIndexChanged(sender, e);

                            ddlCode.SelectedValue = adjustment.ItemCode;
                            ddlCode_SelectedIndexChanged(sender, e);

                            txbAdjustQty.Text = adjustment.QuantityAdjusted.ToString();
                            txbAdjustQty_TextChanged(sender, e); // Update Actual quantity
                            txbReason.Text = adjustment.Reason;

                            DisplayAndLimitQuantity(); // Managing Current quantity

                            txbAdjustmentId.Text = id.ToString();
                            PanelAdjusmentId.Visible = true;
                        }
                        else
                        {
                            ViewState["id"] = null;
                            PanelAdjusmentId.Visible = false;
                            Response.Write("<script>alert('The Adjustment ID in the URL either does not exists or is no longer editable. You would be directed to create a new adjustment.'); </script>");
                        }
                    }
                }
            }
        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDescription.SelectedValue = ddlCode.SelectedValue;
            DisplayAndLimitQuantity();
        }

        protected void ddlDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCode.SelectedValue = ddlDescription.SelectedValue;
            DisplayAndLimitQuantity();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCodeAndDescription();
        }

        void PopulateCodeAndDescription()
        {
            ddlCode.DataSource = saLogic.FindAllStationeryByCategory(Convert.ToInt32(ddlCategory.SelectedValue));
            ddlCode.DataTextField = "ItemCode";
            ddlCode.DataValueField = "ItemCode";
            ddlCode.DataBind();

            ddlDescription.DataSource = saLogic.FindAllStationeryByCategory(Convert.ToInt32(ddlCategory.SelectedValue));
            ddlDescription.DataTextField = "Description";
            ddlDescription.DataValueField = "ItemCode";
            ddlDescription.DataBind();

            DisplayAndLimitQuantity();
        }

        void DisplayAndLimitQuantity()
        {
            int actualQty = saLogic.FindCurrentQuantityByItemCode(ddlCode.SelectedValue);
            txbCurrentQty.Text = actualQty.ToString();
            RangeValidatorAdjusted.MinimumValue = (-actualQty).ToString();
        }

        protected void txbAdjustQty_TextChanged(object sender, EventArgs e)
        {
            if (txbAdjustQty.Text != "")
                txbActualQty.Text = (Convert.ToInt32(txbCurrentQty.Text) + Convert.ToInt32(txbAdjustQty.Text)).ToString();
        }

        protected void txbActualQty_TextChanged(object sender, EventArgs e)
        {
            if (txbActualQty.Text != "")
                txbAdjustQty.Text = (Convert.ToInt32(txbActualQty.Text) - Convert.ToInt32(txbCurrentQty.Text)).ToString();
        }

        protected void btnSwap1_Click(object sender, EventArgs e)
        {
            bool temp;
            temp = txbAdjustQty.ReadOnly;
            txbAdjustQty.ReadOnly = txbActualQty.ReadOnly;
            txbActualQty.ReadOnly = temp;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/AdjustmentsListClerk.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Brand new adjustment
            if (ViewState["id"] == null)
            {
                if (saLogic.AddAdjustment(ReadAllFields(), "In Progress") == 1)
                    Response.Write(scriptSaveSuccess);
                else
                    Response.Write(scriptSaveFail);
            }
            // In Progress adjustment
            else
            {
                if (saLogic.UpdateAdjustment(ReadAllFields(), "In Progress") == 1)
                    Response.Write(scriptSaveSuccess);
                else
                    Response.Write(scriptSaveFail);
            }
            Response.Redirect("~/Store/AdjustmentsListClerk.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Brand new adjustment
            if (ViewState["id"] == null)
            {
                if (saLogic.AddAdjustment(ReadAllFields(), "Pending") == 1)
                    Response.Write(scriptSubmitSuccess);
                else
                    Response.Write(scriptSubmitFail);
            }
            // In Progress adjustment
            else
            {
                if (saLogic.UpdateAdjustment(ReadAllFields(), "Pending") == 1)
                    Response.Write(scriptSubmitSuccess);
                else
                    Response.Write(scriptSubmitFail);
            }
            Response.Redirect("~/Store/AdjustmentsListClerk.aspx");
        }

        StockAdjustment ReadAllFields()
        {
            StockAdjustment adjustment;

            // Brand new adjustment
            if (ViewState["id"] == null)
                adjustment = new StockAdjustment();

            // Existing adjustment
            else
                adjustment = saLogic.FindAdjustmentById((int)ViewState["id"]);

            // For both brand new and existing adjustment
            adjustment.ItemCode = ddlCode.SelectedValue;
            adjustment.QuantityAdjusted = Convert.ToInt32(txbAdjustQty.Text);
            adjustment.Reason = txbReason.Text;
            adjustment.ClerkEmployeeId = (int)Session["employeeId"];

            return adjustment;
        }
    }
}