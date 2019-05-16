/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    public partial class MaintainStockCard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                lblMessage.Text = "";

                if (!IsPostBack)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        string itemCode = Request.QueryString["code"];
                        Stationery stationery = StockManagementBizLogic.getStationery(itemCode);
                        if (stationery != null)
                            PopulateData(stationery);
                        else
                            lblMessage.Text = "No stationery found";
                    }
                    else
                    {
                        gvMaintainStockCard.DataSource = null;
                        gvMaintainStockCard.DataBind();
                        lblMessage.Text = "Please search for an item to display its stock card.";
                    }
                }
            }
        }

        protected void btnMaintainStockCard1_Click(object sender, EventArgs e)
        {
            String itemCode = tbMaintainStockCard.Text;
            Stationery stationery = StockManagementBizLogic.getStationery(itemCode);

            if (stationery != null)
                PopulateData(stationery);
            else
                lblMessage.Text = "No stationery found";
        }

        void PopulateData(Stationery stationery)
        {
            lblItemCode.Text = stationery.ItemCode;
            lblItemDescription.Text = stationery.Description;
            lblBin.Text = stationery.Bin.ToString();
            lblUOM.Text = stationery.UnitOfMeasure;

            Label[] txbSuppliers = { lblSupplier1, lblSupplier2, lblSupplier3 };
            for (int i = 1; i <= 3; i++)
            {
                SupplierStationery ss = stationery.SupplierStationeries.Where(x => x.SupplierRank == i).FirstOrDefault();
                txbSuppliers[i - 1].Text = ss == null ? "NIL" : ss.Supplier.SupplierName;
            }

            int storeClerkId = (int)Session["employeeId"];
            gvMaintainStockCard.DataSource = StockManagementBizLogic.getStockManagements(stationery.ItemCode, storeClerkId);
            gvMaintainStockCard.DataBind();
        }
    }
}