/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    using Models;
    using Services;
    using BizLogic;
    public partial class LowStock : System.Web.UI.Page
    {
        StockBizLogic sLogic = new StockBizLogic();
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
                    List<Stationery> listLowStock = sLogic.FindAllLowStock();
                    if (listLowStock.Count > 0)
                    {
                        gvItems.DataSource = listLowStock;
                        gvItems.DataBind();
                    }
                    else
                        PanelMessage.Visible = true;
                }
            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewCard")
                Response.Redirect("~/Store/MaintainStockCard.aspx?code=" + e.CommandArgument.ToString());
        }
    }
}