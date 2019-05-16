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
    public partial class RequisitionApprovalDetailPage : System.Web.UI.Page
    {
        IRequisitionService ir = new RequisitionService();
        IEmployeeService ie = new EmployeeService();
        IRequisitionDetailService ird = new RequisitionDetailService();
        IStationeryService iss = new StationeryService();
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(authMgmt.IsActingHead(Context.User.Identity.Name) || Context.User.IsInRole("Department Head")))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }

            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["rid"] != null)
                    {
                        string order = Request.QueryString["order"];
                        int eid = int.Parse(Request.QueryString["eid"]);
                        int rid = int.Parse(Request.QueryString["rid"]);
                        ViewState["rid"] = rid;
                        ViewState["order"] = order;

                        lblName.Text = ie.SearchEmployeeByEmpId(eid).EmployeeName;
                        lblReqId.Text = rid.ToString();
                        lblDate.Text = ir.SearchRequisitionbyID(rid).RequisitionDate.ToString("dd MMM yyyy");

                        List<RequisitionDetail> rdl = ird.SearchRequisitionDetailByRequisitionId(rid);
                        List<Stationery> ss = iss.FindAllStationery();
                        var q = from x in rdl
                                join y in ss on x.ItemCode equals y.ItemCode
                                select new { x.StatTransId, y.Description, x.QuantityRequest };
                        GridView1.DataSource = q;
                        GridView1.DataBind();
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int approveid = (int)Session["employeeId"];
            int rid = (int)ViewState["rid"];

            ir.UpdateRequisitionApproval(rid, approveid);
            string rm = TextBox3.Text;
            ir.UpdateRequisitionRemark(rid, rm);
            ir.UpdateRequisitionStatus(rid, "Approved");
            string app = "approved ";

            Response.Redirect("~/Dept/RequisitionApprovalPage.aspx?order=" + (string)ViewState["order"] + "&message=" + app);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int approveid = (int)Session["employeeId"];
            int rid = (int)ViewState["rid"];

            ir.UpdateRequisitionApproval(rid, approveid);
            string rm = TextBox3.Text;
            ir.UpdateRequisitionRemark(rid, rm);
            ir.UpdateRequisitionStatus(rid, "Rejected");
            string rej = "rejected ";

            Response.Redirect("~/Dept/RequisitionApprovalPage.aspx?order=" + (string)ViewState["order"] + "&message=" + rej);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dept/RequisitionApprovalPage.aspx?order=" + (string)ViewState["order"]);
        }
    }
}