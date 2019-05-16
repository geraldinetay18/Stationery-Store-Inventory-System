/* Author: Priyanga Thiruneelakandan */

using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    public partial class InventoryStatusReport : System.Web.UI.Page
    {
        IStationeryService iss= new StationeryService();
        ICategoryService ics = new CategoryService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Manager"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                gvInventoryStatusReport.DataSource = iss.FindAllStationery();
                gvInventoryStatusReport.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvInventoryStatusReport.DataSource = iss.FindStationeryByName(tbSearch.Text);
            gvInventoryStatusReport.DataBind();
        }

        protected string FindCategoryName(string itemCode)
        {
            return ics.FindCategoryById(iss.FindStationeryById(itemCode).CategoryId).CategoryName;
        }
    }
}