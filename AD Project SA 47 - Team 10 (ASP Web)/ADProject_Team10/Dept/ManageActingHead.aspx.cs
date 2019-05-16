/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    public partial class ManageActingHead : System.Web.UI.Page
    {
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();
        string userName;
        string deptId;
        int currentEmpId;
        Employee actingHead;
        TemporaryRole temporaryRole;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Department Head"))
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
            // Bind current acting director
            actingHead = authMgmt.SearchCurrentActingHead(deptId);
            if (actingHead != null)
            {
                temporaryRole = authMgmt.SearchAssignmentByEmpId(actingHead.EmployeeId);
                tbEmpName.Text = actingHead.EmployeeName;
                tbStartDate.Text = temporaryRole.StartDate.ToString("yyyy-MM-dd");
                tbEndDate.Text = temporaryRole.EndDate.ToString("yyyy-MM-dd");
                hfActingHeadEmployeeId.Value = actingHead.EmployeeId.ToString();
                btnRemove.Visible = true;
                btnUpdate.Visible = true;
                btnDelegate.Visible = false;

                if (DateTime.Compare(DateTime.Now.Date, temporaryRole.StartDate.Date) > 0)
                {
                    tbStartDate.Enabled = false;
                    tbEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    tbStartDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                    tbEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                tbEmpName.Text = "";
                tbStartDate.Text = "";
                tbEndDate.Text = "";
                hfActingHeadEmployeeId.Value = "";
                btnRemove.Visible = false;
                btnUpdate.Visible = false;
                btnDelegate.Visible = true;

                tbStartDate.Enabled = true;
                tbStartDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                tbEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            }

            BindGridView();
        }

        private void BindGridView()
        {
            // Bind employee list under this department head excluding existing acting head
            List<Employee> empList = PopulateEmpList();
            gvStaffList.DataSource = empList;
            gvStaffList.DataBind();
            if (empList.Count == 0)
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "No employee found in this department.";
            }
        }

        private List<Employee> PopulateEmpList()
        {
            return !Page.IsPostBack ? authMgmt.ListEmplyUnderSameDeptExclActHead(userName) : authMgmt.SearchEmployeeUnderSameDeptByEmpNameExclActHead(userName, txtNameSearch.Text);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int actingHeadEmpId = Convert.ToInt32(hfActingHeadEmployeeId.Value);
            int result = authMgmt.DeleteTemporaryAssignment(actingHeadEmpId);
            if (result != 0)
            {
                BindGrid();
                PanelGoodMessage.Visible = true;
                lblGoodMessage.Text = "The delegation of Current Acting Department Head has been removed. You may make a new delegation now.";
            }
            else
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "The removal of delegation of Current Acting Department Head is unsuccessful. Please try again.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result;
            int actingHeadEmpId = Convert.ToInt32(hfActingHeadEmployeeId.Value);
            TemporaryRole existingTemporaryRole = authMgmt.SearchAssignmentByEmpId(actingHeadEmpId);

            DateTime StartDate, EndDate;
            if (!DateTime.TryParse(tbStartDate.Text, out StartDate) || !DateTime.TryParse(tbEndDate.Text, out EndDate))
            {
                result = 0;
            }
            else
            {
                existingTemporaryRole.StartDate = StartDate;
                existingTemporaryRole.EndDate = EndDate;
            }

            result = authMgmt.UpdateTemporaryAssignment(existingTemporaryRole);

            if (result > 0)
            {
                PanelGoodMessage.Visible = true;
                lblGoodMessage.Text = "The delegation of Current Acting Department Head has been updated";
                BindGrid();
            }
            else
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "The update of delegation of Current Acting Department Head is unsuccessful. Please try again.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            BindGridView();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<Employee> empSearchList = authMgmt.SearchEmployeeUnderSameDeptByEmpNameExclActHead(userName, txtNameSearch.Text);
            gvStaffList.DataSource = empSearchList;
            gvStaffList.DataBind();

            if (empSearchList.Count == 0)
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "'0' search result. Please enter another name.";
            }
        }

        protected void gvStaffList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                hfSelectedEmployeeId.Value = e.CommandArgument.ToString();
                int empId = Convert.ToInt32(hfSelectedEmployeeId.Value);
                Employee emp = authMgmt.SearchEmployeeByEmpId(empId);
                if (string.IsNullOrEmpty(hfActingHeadEmployeeId.Value))
                {
                    tbEmpName.Text = emp.EmployeeName;
                }
            }
        }
        protected void gvStaffList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStaffList.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void gvStaffList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.FindControl("btnSelect");
                if (!string.IsNullOrEmpty(hfActingHeadEmployeeId.Value))
                {
                    btn.Visible = false;
                }
            }
        }

        protected void btnDelegate_Click(object sender, EventArgs e)
        {
            int result;
            TemporaryRole newTemporaryRole = new TemporaryRole();
            newTemporaryRole.TemporaryRoleId = "ActHead";
            newTemporaryRole.EmployeeId = Convert.ToInt32(hfSelectedEmployeeId.Value);

            DateTime StartDate, EndDate;
            if (!DateTime.TryParse(tbStartDate.Text, out StartDate) || !DateTime.TryParse(tbEndDate.Text, out EndDate))
            {
                result = 0;
            }
            else
            {
                newTemporaryRole.StartDate = StartDate;
                newTemporaryRole.EndDate = EndDate;
            }

            result = authMgmt.CreateTemporaryAssignment(newTemporaryRole);

            if (result != 0)
            {
                PanelGoodMessage.Visible = true;
                lblGoodMessage.Text = "The delegation of Acting Department Head is successful.";
                BindGrid();
            }
            else
            {
                PanelFailMessage.Visible = true;
                lblFailMessage.Text = "The delegation of Acting Department Head is unsuccessful. Please try again.";
            }
        }
    }
}