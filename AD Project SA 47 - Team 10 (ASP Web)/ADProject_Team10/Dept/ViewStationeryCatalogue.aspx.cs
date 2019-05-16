/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    public partial class ViewStationeryCatalogue : System.Web.UI.Page
    {
        IStationeryService iss = new StationeryService();
        IEmployeeService ies = new EmployeeService();
        IRequisitionService irs = new RequisitionService();
        Requisition requests;
        List<RequisitionDetail> requisitionDetails;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Department Staff"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                gvStationeryCatalogue.DataSource = iss.FindStationeryByCatagoryName(ddlStationery.SelectedValue).ToList();
                gvStationeryCatalogue.DataBind();
                int employeeId = (int)Session["employeeId"];
                int requisitionEmployeeId = (int)Session["requisitionEmployeeId"];

                if (employeeId == requisitionEmployeeId)
                {
                    requests = (Requisition)Session["Requests"];
                    requisitionDetails = Session["RequestsDetails"] as List<RequisitionDetail>;

                    Requisition request = new Requisition()
                    {
                        RequisitionStatus = "Pending",
                        EmployeeId = employeeId
                    };
                    requests = request;
                    Session["Requests"] = request;
                }
                else
                {
                    Session["Requests"] = new Requisition();
                    Session["RequestsDetails"] = new List<RequisitionDetail>();
                    Session["requisitionEmployeeId"] = employeeId;
                    requests = (Requisition)Session["Requests"];
                    requisitionDetails = Session["RequestsDetails"] as List<RequisitionDetail>;
                    Requisition r = irs.GetLastRequisitionId();
                    Requisition request = new Requisition()
                    {
                        RequisitionStatus = "Pending",
                        EmployeeId = employeeId
                    };
                    requests = request;
                    Session["Requests"] = request;
                }
            }
        }
        protected void ddlStationery_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvStationeryCatalogue.DataSource = iss.FindStationeryByCatagoryName(ddlStationery.SelectedValue);
                gvStationeryCatalogue.DataBind();
            }
            catch (Exception ex)
            {
                string errormsg = string.Format("<script>Error:{0}</script>", ex.Message);
                Response.Write(errormsg);
            }
        }

        protected void gvStationeryCatalogue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToRequest")
            {
                requisitionDetails = Session["RequestsDetails"] as List<RequisitionDetail>;
                bool duplicate = false;
                Stationery s = iss.FindStationeryById(e.CommandArgument.ToString());

                foreach (RequisitionDetail rd in requisitionDetails)
                {
                    if (rd.ItemCode == s.ItemCode)
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (!duplicate)
                {
                    RequisitionDetail rd = new RequisitionDetail()
                    {
                        ItemCode = s.ItemCode,
                        QuantityRequest = 1
                    };
                    requisitionDetails.Add(rd);
                    Session["RequestsDetails"] = requisitionDetails;
                    lblItem.Text = s.Description + " is added to your Requisition Form";
                }
                else
                    lblItem.Text = s.Description + " has already been added to your Requisition Form.";
            }
        }
    }
}