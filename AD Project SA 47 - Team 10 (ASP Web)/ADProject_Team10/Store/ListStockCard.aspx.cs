/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    using Services;
    using Models;
    using BizLogic;
    public partial class ListStockCard : System.Web.UI.Page
    {
        IStationeryService iss = new StationeryService();
        ICategoryService ics = new CategoryService();
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
                    gvItems.DataSource = iss.FindAllStationery();
                    gvItems.DataBind();
                }
            }
        }

        protected string FindCategoryName(string itemCode)
        {
            return ics.FindCategoryById(iss.FindStationeryById(itemCode).CategoryId).CategoryName;
        }

        protected void btnMaintainStockCard1_Click(object sender, EventArgs e)
        {
            String itemCode = tbMaintainStockCard.Text;
            List<Stationery> listOne = new List<Stationery>();
            Stationery stationery = StockManagementBizLogic.getStationery(itemCode);

            if (stationery != null)
            {
                listOne.Add(stationery);
                gvItems.DataSource = listOne;
                gvItems.DataBind();
            }
            else
                lblMessage.Text = "No stationery found";
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string itemcode = (string)DataBinder.Eval(e.Row.DataItem, "ItemCode");
            if (e.Row.RowType == DataControlRowType.DataRow && iss.ValidateLowStock(itemcode))
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewCard")
            {
                Response.Redirect("~/Store/MaintainStockCard.aspx?code=" + e.CommandArgument.ToString());
            }
        }
    }
}