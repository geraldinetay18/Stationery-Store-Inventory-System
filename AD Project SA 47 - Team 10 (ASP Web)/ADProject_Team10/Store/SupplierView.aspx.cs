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
    public partial class SupplierView : System.Web.UI.Page
    {
        static SupplierBizLogic suLogic = new SupplierBizLogic();

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
                    // Directed from "View" in list
                    if (Request.QueryString["code"] != null)
                    {
                        // Get supplier
                        string code = Request.QueryString["code"];
                        ViewState["code"] = code;
                        Supplier s = suLogic.FindSupplierById(code);

                        // Validate if supplier exists
                        if (s != null)
                        {
                            // Header
                            lblName.Text = s.SupplierName;
                            lblCode.Text = s.SupplierCode;
                            lblGST.Text = s.GSTRegistrationNo;
                            lblAddress.Text = s.Address;
                            lblContactPerson.Text = s.ContactName;
                            lblPhone.Text = s.Phone;
                            lblFax.Text = s.Fax;

                            // Items
                            List<SupplierStationery> listItems = suLogic.FindStationeryDetailOfSupplier(code);
                            gvItems.DataSource = listItems;
                            gvItems.DataBind();
                            lblTotal.Text = listItems.Count.ToString();
                        }
                        else
                        {
                            PanelMessage.Visible = true;
                        }
                    }
                    else
                    {
                        PanelMessage.Visible = true;
                    }
                }
            }
        }

        protected string FindDescription(string itemCode)
        {
            return suLogic.FindDescription(itemCode);
        }

        protected string FindCategoryNameOfItem(string itemCode)
        {
            return suLogic.FindStationeryById(itemCode).Category.CategoryName;   
        }
    }
}