/* Author: Sun Chengyuan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace ADProject_Team10.Dept
{
    using Services;
    using Models;
    using BizLogic;
    public partial class SubmitRequest : System.Web.UI.Page
    {
        IRequisitionService ir = new RequisitionService();
        IEmployeeService ie = new EmployeeService();
        IRequisitionDetailService ird = new RequisitionDetailService();
        IDepartmentService id = new DepartmentService();
        IStationeryService iss = new StationeryService();
        RequisitionBizLogic rLogic = new RequisitionBizLogic();

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
                    List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;

                    if (requisitiondetail.Count > 0)
                    {
                        List<Stationery> ss = iss.FindAllStationery();
                        var q = from x in requisitiondetail
                                join y in ss on x.ItemCode equals y.ItemCode
                                select new { y.Description, y.UnitOfMeasure, x.QuantityRequest };

                        GridView1.DataSource = q;
                        GridView1.DataBind();
                    }
                    else
                    {
                        PanelMessage.Visible = true;
                        PanelAll.Visible = false;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;

            if (requisitiondetail.Count() == 0)
            {
                Response.Write("<script>alert('No Items,Request failed!');</script>");
            }
            else
            {
                int empId = (int)Session["employeeId"];
                if (rLogic.SubmitRequisition(empId, requisitiondetail) == 1)
                    Response.Write("<script>alert('Request Succeed!');</script>");
                else
                    Response.Write("<script>alert('Request Fail!'</script>");

                // Clear Session and display
                btnClear_Click(sender, e);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;

            GridView1.EditIndex = e.NewEditIndex;
            List<Stationery> ss = iss.FindAllStationery();
            var q = from x in requisitiondetail
                    join y in ss on x.ItemCode equals y.ItemCode
                    select new { x.StatTransId, x.ItemCode, y.Description, y.UnitOfMeasure, x.QuantityRequest };
            GridView1.DataSource = q;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;

            int Index = e.RowIndex;
            int sid = requisitiondetail[Index].StatTransId;
            requisitiondetail.RemoveAt(Index);
            List<Stationery> ss = iss.FindAllStationery();
            var q = from x in requisitiondetail
                    join y in ss on x.ItemCode equals y.ItemCode
                    select new { x.StatTransId, x.ItemCode, y.Description, y.UnitOfMeasure, x.QuantityRequest };
            GridView1.DataSource = q;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;

            GridView1.EditIndex = -1;
            List<Stationery> ss = iss.FindAllStationery();
            var q = from x in requisitiondetail
                    join y in ss on x.ItemCode equals y.ItemCode
                    select new { x.StatTransId, x.ItemCode, y.Description, y.UnitOfMeasure, x.QuantityRequest };
            GridView1.DataSource = q;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;

            GridView1.EditIndex = -1;
            TextBox t = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
            try
            {

                if (Convert.ToInt32(t.Text) > iss.FindStationeryById(requisitiondetail[e.RowIndex].ItemCode).QuantityInStock)
                {
                    Response.Write("<script>alert('The quantity typed is beyond the stock`s quantity!');</script>");
                    Response.Write("<script language=javascript>history.go(-1);</script>");
                }
                else if (Convert.ToInt32(t.Text) <= 0)
                {
                    Response.Write("<script>alert('Illegal input!');</script>");
                    Response.Write("<script language=javascript>history.go(-1);</script>");
                }
                else
                {
                    requisitiondetail.ElementAt(e.RowIndex).QuantityRequest = Convert.ToInt32(t.Text);
                    List<Stationery> ss = iss.FindAllStationery();
                    var q = from x in requisitiondetail
                            join y in ss on x.ItemCode equals y.ItemCode
                            select new { x.StatTransId, x.ItemCode, y.Description, y.UnitOfMeasure, x.QuantityRequest };

                    GridView1.DataSource = q;
                    GridView1.DataBind();
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Illegal input!');</script>");
                Response.Write("<script language=javascript>history.go(-1);</script>");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            List<RequisitionDetail> requisitiondetail = Session["RequestsDetails"] as List<RequisitionDetail>;
            requisitiondetail.Clear();
            GridView1.DataSource = requisitiondetail;
            GridView1.DataBind();
            Session["RequestsDetails"] = requisitiondetail;

            PanelMessage.Visible = true;
            PanelAll.Visible = false;
        }
    }
}