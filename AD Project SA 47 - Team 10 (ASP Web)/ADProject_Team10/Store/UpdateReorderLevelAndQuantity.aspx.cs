/* Authors: 
 * Priyanga Thiruneelakandan 
 * Shalin Christina Stephen Selvaraja 
 */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    public partial class UpdateReorderLevelAndQuantity : System.Web.UI.Page
    {
        int recommendedLevel;
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
                lblError.Text = "";
                if (!IsPostBack)
                {
                    gvUpadateReorderLevelQuantity.DataSource = iss.FindAllStationery();
                    gvUpadateReorderLevelQuantity.DataBind();
                }
            }
        }
        protected string FindCategoryName(string itemCode)
        {
            return ics.FindCategoryById(iss.FindStationeryById(itemCode).CategoryId).CategoryName;
        }
        protected int FindRecomLevel(int RecommandQuantity, int QuantityReorder, int ReorderLevel)
        {
            if (RecommandQuantity > QuantityReorder)
            {
                recommendedLevel = RecommandQuantity - QuantityReorder;
                recommendedLevel += ReorderLevel;
            }
            else
            {
                recommendedLevel = ReorderLevel;
            }
            return recommendedLevel;
        }
        protected void gvUpadateReorderLevelQuantity_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUpadateReorderLevelQuantity.EditIndex = e.NewEditIndex;
        }

        protected void gvUpadateReorderLevelQuantity_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUpadateReorderLevelQuantity.Rows[e.RowIndex];
            Stationery s = new Stationery();
            s.ItemCode = (row.FindControl("ItemCode") as Label).Text;
            Stationery source = iss.FindStationeryById(s.ItemCode);
            s.QuantityInStock = source.QuantityInStock;
            s.OverRequestFrequency = source.OverRequestFrequency;
            s.CategoryId = source.CategoryId;
            s.Description = source.Description;
            s.UnitOfMeasure = source.UnitOfMeasure;
            s.Bin = source.Bin;
            s.AdjustmentRemark = source.AdjustmentRemark;
            s.RecommandQuantity = source.RecommandQuantity;
            s.QuantityReorder = Convert.ToInt32((row.FindControl("QuantityReorder") as TextBox).Text);
            s.ReorderLevel = Convert.ToInt32((row.FindControl("ReorderLevel") as TextBox).Text);
            iss.UpdateStationery(s);
            gvUpadateReorderLevelQuantity.EditIndex = -1;
            tbSearch.Text = "";
            gvUpadateReorderLevelQuantity.DataSource = iss.FindAllStationery();
            gvUpadateReorderLevelQuantity.DataBind();
        }
        protected void gvUpadateReorderLevelQuantity_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gvUpadateReorderLevelQuantity.EditIndex = -1;
            if ((tbSearch.Text) != "")
            {
                Bind();
            }
            else
            {
                gvUpadateReorderLevelQuantity.DataSource = iss.FindAllStationery();
                gvUpadateReorderLevelQuantity.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }
        protected void Bind()
        {
            Stationery stat;
            List<Stationery> list = new List<Stationery>();

            if (ddlCatDes.SelectedValue == "ItemCode")
            {
                stat = iss.FindStationeryById(tbSearch.Text.Trim());
                if (stat == null)
                {
                    BindNull();
                }
                else
                {
                    list.Add(stat);
                    gvUpadateReorderLevelQuantity.DataSource = list;
                    gvUpadateReorderLevelQuantity.DataBind();
                }
            }
            else if(ddlCatDes.SelectedValue=="ViewAll")
            {
                tbSearch.Text = "";
                gvUpadateReorderLevelQuantity.DataSource = iss.FindAllStationery();
                gvUpadateReorderLevelQuantity.DataBind();
            }
            else
            {
                list = iss.FindStationeryByName(tbSearch.Text);
                if (list.Count <1)
                {
                    BindNull();
                }
                else
                {
                    gvUpadateReorderLevelQuantity.DataSource = list;
                    gvUpadateReorderLevelQuantity.DataBind();
                }
            }
        }
        protected void BindNull()
        {
            tbSearch.Text = "";
            gvUpadateReorderLevelQuantity.DataSource = null;
            gvUpadateReorderLevelQuantity.DataBind();
            lblError.Text = "No such Record Found / Check the dropdown selection";
        }
    }
}