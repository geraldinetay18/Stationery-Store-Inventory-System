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
    public partial class ViewRequisitionHistoryPage : System.Web.UI.Page
    {
        IRequisitionService ir = new RequisitionService();
        IEmployeeService ie = new EmployeeService();
        IRequisitionDetailService ird = new RequisitionDetailService();
        IStationeryService iss = new StationeryService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Context.User.IsInRole("Department Staff") || Context.User.IsInRole("Department Head")))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                PanelMessage.Visible = false;

                if (!IsPostBack)
                {
                    List<Requisition> rl = ir.ListAllRequisition();
                    string deptId = (ie.SearchEmployeeByEmpId((int)Session["employeeId"])).DeptId;
                    List<Employee> ee = ie.SearchEmployeeByDeptId(deptId);

                    var q = from x in rl
                            join y in ee on x.EmployeeId equals y.EmployeeId
                            select new { x.RequisitionId, y.EmployeeName, x.RequisitionDate, x.RequisitionStatus };

                    if (q.ToList().Count > 0)
                    {
                        GridView1.DataSource = q;
                        GridView1.DataBind();
                    }
                    else
                        PanelMessage.Visible = true;
                }

                Label7.Visible = false;
                GridView1.Visible = Visible;
                PanelForm.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label7.Visible = false;
            string eName = TextBox3.Text;

            string deptId = (ie.SearchEmployeeByEmpId((int)Session["employeeId"])).DeptId;
            List<Requisition> rl = ir.SearchRequisitionbyTwoDate(Convert.ToDateTime(TextBox1.Text), Convert.ToDateTime(TextBox2.Text));
            List<Employee> emp;

            //All 3 fields --> Filter to selected employees of dept
            if (TextBox3.Text != "")
                emp = ie.SearchEmployeeByDeptIdAndEmpName(deptId, eName);

            else
                emp = ie.SearchEmployeeByDeptId(deptId);

            var q = from x in emp
                    join y in rl on x.EmployeeId equals y.EmployeeId
                    select new { y.RequisitionId, x.EmployeeName, y.RequisitionDate, y.RequisitionStatus };

            if (q.Count() == 0)
            {
                Label7.Visible = Visible;
                GridView1.Visible = false;
            }
            else
            {
                GridView1.DataSource = q;
                GridView1.DataBind();
                GridView1.Visible = Visible;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                PanelForm.Visible = true;

                string[] com = e.CommandArgument.ToString().Split(new char[] { ',' });
                int rid = int.Parse(com[0]);
                string eName = com[1];
                lblName.Text = eName;
                lblReqId.Text = com[0];

                Requisition r = ir.SearchRequisitionbyID(rid);
                lblStatus.Text = r.RequisitionStatus;

                switch (r.RequisitionStatus.ToLower())
                {
                    case "approved": lblStatus.ForeColor = System.Drawing.Color.Green; break;
                    case "rejected": lblStatus.ForeColor = System.Drawing.Color.Red; break;
                    default: lblStatus.ForeColor = System.Drawing.Color.Orange; break; // case "Pending"
                }

                lblDate.Text = string.Format("{0: dd MMM yyyy}", r.RequisitionDate);
                if (r.ApprovedByEmployeeId != null)
                    lblApprovedBy.Text = ie.SearchEmployeeByEmpId((int)r.ApprovedByEmployeeId).EmployeeName;
                else
                    lblApprovedBy.Text = "None yet";

                if (ie.SearchEmployeeByEmpName(eName).Count() > 0)
                {
                    string eid = ie.SearchEmployeeByEmpName(eName).FirstOrDefault().EmployeeId.ToString();
                    List<RequisitionDetail> rdl = ird.SearchRequisitionDetailByRequisitionId(rid).ToList();
                    List<Stationery> sl = iss.FindAllStationery();
                    var q = from x in rdl
                            join y in sl on x.ItemCode equals y.ItemCode
                            select new { y.Description, x.QuantityRequest };
                    GridView2.DataSource = q;
                    GridView2.DataBind();
                    GridView2.Visible = Visible;
                }
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            List<Requisition> rl = ir.ListAllRequisition();
            string deptId = (ie.SearchEmployeeByEmpId((int)Session["employeeId"])).DeptId;
            List<Employee> ee = ie.SearchEmployeeByDeptId(deptId);

            var q = from x in rl
                    join y in ee on x.EmployeeId equals y.EmployeeId
                    select new { x.RequisitionId, y.EmployeeName, x.RequisitionDate, x.RequisitionStatus };

            if (q.ToList().Count > 0)
            {
                GridView1.DataSource = q;
                GridView1.DataBind();
            }
            else
                PanelMessage.Visible = true;

            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Color-code status
                string status = ((string)DataBinder.Eval(e.Row.DataItem, "RequisitionStatus")).ToLower();
                switch (status)
                {
                    case "approved": e.Row.Cells[2].ForeColor = System.Drawing.Color.Green; break;
                    case "rejected": e.Row.Cells[2].ForeColor = System.Drawing.Color.Red; break;
                    default: e.Row.Cells[2].ForeColor = System.Drawing.Color.Orange; break; // case "Pending"
                }
            }
        }
    }
}