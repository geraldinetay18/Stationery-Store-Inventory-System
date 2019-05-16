/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    public partial class ManageDeptRep : Page
    {
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();
        string userName;
        string deptId;
        int currentEmpId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(authMgmt.IsActingHead(Context.User.Identity.Name) || Context.User.IsInRole("Department Head")))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                userName = Context.User.Identity.Name;
                deptId = (string)Session["deptId"];
                currentEmpId = (int)Session["employeeId"];

                PanelGoodMessage.Visible = false;
                PanelFailMessage.Visible = false;

                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            // Bind current department rep
            Employee deptRep = authMgmt.SearchCurrentDeptRep(deptId);
            if (deptRep != null)
            {
                lblName.Text = deptRep.EmployeeName;
            }
            else
            {
                lblName.Text = "No department representative is delegated";
            }

            BindGridView();
        }

        private void BindGridView()
        {
            // Bind employee list under this department head excluding Dept Rep
            List<Employee> empList = !Page.IsPostBack ? authMgmt.ListEmplyUnderSameDeptExclDeptRep(userName) : authMgmt.SearchEmployeeUnderSameDeptByEmpNameExclDeptRep(userName, txtNameSearch.Text);
            gvStaffList.DataSource = empList;
            gvStaffList.DataBind();
            if (empList.Count == 0)
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "No employee found in this department.";
            }
        }

        protected void gvStaffList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Replace")
            {
                int empId = Convert.ToInt32(e.CommandArgument.ToString());
                int result = authMgmt.UpdateCurrentDeptRep(deptId, empId);
                if (result == 1)
                {
                    BindGrid();
                    PanelGoodMessage.Visible = true;
                    lblGoodMessage.Text = "Replace Successfully.";
                }
                else
                {
                    PanelFailMessage.Visible = true;
                    lblFailMessage.Text = "Replace Failed.";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<Employee> empSearchList = authMgmt.SearchEmployeeUnderSameDeptByEmpNameExclDeptRep(userName, txtNameSearch.Text);
            gvStaffList.DataSource = empSearchList;
            gvStaffList.DataBind();

            if (empSearchList.Count == 0)
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "'0' search result. Please enter another name.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            BindGridView();
        }

        protected void gvStaffList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.FindControl("btnReplace");
                btn.Attributes.Add("onclick", "return confirm('Are you sure to replace current assignment?');");
            }
        }

        protected void gvStaffList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStaffList.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}