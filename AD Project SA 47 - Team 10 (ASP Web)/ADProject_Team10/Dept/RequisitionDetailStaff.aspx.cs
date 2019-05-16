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
    using Models;
    using BizLogic;

    public partial class RequisitionDetailStaff : System.Web.UI.Page
    {
        IRequisitionService ir = new RequisitionService();
        IEmployeeService ie = new EmployeeService();
        IRequisitionDetailService ird = new RequisitionDetailService();
        IStationeryService iss = new StationeryService();

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
                    // A particular requisition object was selected
                    if (Request.QueryString["rid"] != null)
                    {
                        int rid = int.Parse(Request.QueryString["rid"]);
                        ViewState["rid"] = rid;

                        // Header
                        Requisition r = ir.SearchRequisitionbyID(rid);
                        lblReqId.Text = rid.ToString();
                        lblDate.Text = r.RequisitionDate.ToString("dd MMM yyyy");
                        lblStatus.Text = r.RequisitionStatus;
                        lblTotal.Text = ird.GetTotalQuantityByRequisitionId(rid).ToString();
                        lblRemark.Text = (r.Remark == null || r.Remark == "") ? "NIL" : r.Remark;
                        switch (r.RequisitionStatus.ToLower())
                        {
                            case "approved": lblStatus.ForeColor = System.Drawing.Color.Green; break;
                            case "rejected": lblStatus.ForeColor = System.Drawing.Color.Red; break;
                            default: lblStatus.ForeColor = System.Drawing.Color.Orange; break; // case "Pending"
                        }

                        // Items
                        List<RequisitionDetail> rdl = ird.SearchRequisitionDetailByRequisitionId(rid);
                        List<Stationery> ss = iss.FindAllStationery();
                        var q = from x in rdl
                                join y in ss on x.ItemCode equals y.ItemCode
                                select new { x.StatTransId, y.Description, x.QuantityRequest };
                        GridView1.DataSource = q;
                        GridView1.DataBind();
                    }
                    else
                        Response.Redirect("~/Dept/RequisitionListStaff.aspx");
                }
            }
        }
    }
}