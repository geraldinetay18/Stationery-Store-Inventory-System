/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    using Services;
    using BizLogic;
    public partial class RequisitionListStaff : System.Web.UI.Page
    {
        IEmployeeService ies = new EmployeeService();
        IRequisitionService ir = new RequisitionService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Department Staff"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (ir.SearchRequisitionbyEmployeeId((int)Session["employeeId"]).Count == 0)
                        PanelMessage.Visible = true;
                }
            }
        }
        protected string FindEmployeeName(int empId)
        {
            return ies.SearchEmployeeByEmpId(empId).EmployeeName;
        }

        protected void gvRequisitions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Color-code status
                string status = ((string)DataBinder.Eval(e.Row.DataItem, "RequisitionStatus")).ToLower();
                switch (status)
                {
                    case "approved": e.Row.Cells[3].ForeColor = System.Drawing.Color.Green; break;
                    case "rejected": e.Row.Cells[3].ForeColor = System.Drawing.Color.Red; break;
                    default: e.Row.Cells[3].ForeColor = System.Drawing.Color.Orange; break; // case "Pending"
                }
            }
        }

        protected void gvRequisitions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                Response.Redirect("~/Dept/RequisitionDetailStaff.aspx?rid=" + e.CommandArgument.ToString());
            }
        }
    }
}