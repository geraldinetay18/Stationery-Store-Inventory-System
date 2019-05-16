/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;
using ADProject_Team10.Services;

namespace ADProject_Team10.Store
{
    public partial class StoreSupAssignDepartment : System.Web.UI.Page
    {
        SSAEntities ssae = new SSAEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Supervisor"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    DataBind();
                    AssignStoreClerkGridView.DataSource = AssignDepartment.getStoreClerkDepartmentList();
                    AssignStoreClerkGridView.DataBind();
                }
            }
        }

        private void BindGrid()
        {
            AssignStoreClerkGridView.DataSource = AssignDepartment.getStoreClerkDepartmentList();
            AssignStoreClerkGridView.DataBind();
        }

        protected void OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            AssignStoreClerkGridView.EditIndex = -1;
            BindGrid();
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            AssignStoreClerkGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = AssignStoreClerkGridView.Rows[e.RowIndex];
            int storeClerkId = Convert.ToInt32((row.FindControl("StoreClerkIdList") as DropDownList).Text);
            String deptId = (row.FindControl("DeptIdTextBox") as Label).Text;

            AssignDepartment.updateStoreClerkDepartment(storeClerkId, deptId);

            AssignStoreClerkGridView.EditIndex = -1;
            AssignStoreClerkGridView.DataSource = AssignDepartment.getStoreClerkDepartmentList();
            BindGrid();

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated successfully!');", true);
        }
    }
}