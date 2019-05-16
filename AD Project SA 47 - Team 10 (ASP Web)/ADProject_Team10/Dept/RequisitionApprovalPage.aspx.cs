/* Author: Sun Chengyuan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    using Services;
    using Models;
    using BizLogic;

    public partial class RequisitionApprovalPage : System.Web.UI.Page
    {
        static string order;
        IRequisitionService ir = new RequisitionService();
        IEmployeeService ie = new EmployeeService();
        IRequisitionDetailService ird = new RequisitionDetailService();
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(authMgmt.IsActingHead(Context.User.Identity.Name) || Context.User.IsInRole("Department Head")))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                PanelMessage.Visible = false;

                if (Request.QueryString["message"] != null)
                {
                    PanelMessage.Visible = true;
                    lblApproveOrReject.Text = (string)Request.QueryString["message"];
                }

                if (!IsPostBack)
                {
                    // Retrieve requisitions of this department
                    string deptId = (ie.SearchEmployeeByEmpId((int)Session["employeeId"])).DeptId;
                    ViewState["deptId"] = deptId;

                    // Retain filter if redirected fr approval details page by modifying q
                    string order = Request.QueryString["order"];
                    if (order != null)
                        DisplayRequisitionsList(order);
                    else
                        DisplayRequisitionsList();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                string[] com = e.CommandArgument.ToString().Split(new char[] { ',' });
                int rid = int.Parse(com[0]);
                int eid = int.Parse(com[1]);

                Response.Redirect("~/Dept/RequisitionApprovalDetailPage.aspx?eid=" + eid + "&rid=" + rid + "&order=" + order);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayRequisitionsList(DropDownList1.SelectedValue);
        }

        void DisplayRequisitionsList(string order)
        {
            string deptId = (string)ViewState["deptId"];
            List<Requisition> rd = ir.GetPendingRequisitionListOfDept(deptId);
            List<Employee> ee = ie.SearchEmployeeByDeptId(deptId);
            var q = from x in rd
                    join y in ee on x.EmployeeId equals y.EmployeeId
                    select new { x.RequisitionId, x.EmployeeId, y.EmployeeName, x.RequisitionDate, x.RequisitionStatus, x.Remark };

            switch (order)
            {
                case "Date":
                    q = from x in rd
                        join y in ee on x.EmployeeId equals y.EmployeeId
                        orderby x.RequisitionStatus descending
                        select new { x.RequisitionId, x.EmployeeId, y.EmployeeName, x.RequisitionDate, x.RequisitionStatus, x.Remark };
                    break;
                case "Employee Name":
                    q = from x in ee
                        join y in rd on x.EmployeeId equals y.EmployeeId
                        orderby x.EmployeeName
                        select new { y.RequisitionId, y.EmployeeId, x.EmployeeName, y.RequisitionDate, y.RequisitionStatus, y.Remark };
                    break;
            }

            // Display requisition list
            if (q.ToList().Count > 0)
            {
                GridView1.DataSource = q;
                GridView1.DataBind();
            }
            else
            {
                PanelNoPending.Visible = true;
                PanelAll.Visible = false;
            }
        }

        void DisplayRequisitionsList()
        {
            string deptId = (string)ViewState["deptId"];
            List<Requisition> rd = ir.GetPendingRequisitionListOfDept(deptId);
            List<Employee> ee = ie.SearchEmployeeByDeptId(deptId);

            var q = from x in rd
                    join y in ee on x.EmployeeId equals y.EmployeeId
                    select new { x.RequisitionId, x.EmployeeId, y.EmployeeName, x.RequisitionDate, x.RequisitionStatus, x.Remark };

            // Display requisition list
            if (q.ToList().Count > 0)
            {
                GridView1.DataSource = q;
                GridView1.DataBind();
            }
            else
            {
                PanelNoPending.Visible = true;
                PanelAll.Visible = false;
            }
        }
    }
}