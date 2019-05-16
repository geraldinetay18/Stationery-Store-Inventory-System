/* Author: Priyanga Thiruneelakandan */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;
using System.IO;

namespace ADProject_Team10.Store
{
    public partial class ChargeBack : System.Web.UI.Page
    {
        IStationeryService iss = new StationeryService();
        ICategoryService ics = new CategoryService();
        IEmployeeService ies = new EmployeeService();
        IDepartmentService ids = new DepartmentService();
        IDisbursementService idis = new DisbursementService();
        ISupplierService isss = new SupplierService();
        static int requestq;
        static double up;
        static string depId;
        int dis;
        double cost = 0;
        static DateTime fromDate;
        static DateTime toDate;
        List<DisbursementDetail> dd;
        Employee deptHead;
        Employee loggedInUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Manager"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    gvChargeBack.DataSource = null;
                    gvChargeBack.DataBind();
                }
                loggedInUser = ies.SearchEmployeeByEmpId((int)Session["employeeId"]);
                lblEmail.Text = ies.SearchEmployeeByEmpId(ids.SearchDepartmentByName(ddlDepartment.SelectedValue).HeadId).Email;
            }
        }

        protected string FindCategoryName(string itemCode)
        {
            return ics.FindCategoryById(iss.FindStationeryById(itemCode).CategoryId).CategoryName;
        }
        protected string FindDescriptionName(string itemcode)
        {
            return iss.FindStationeryById(itemcode).Description;
        }
        protected int FindQuanRecv(string itemCode)
        {

            dis = idis.GetDisbursingDisburmentByDeptId(depId).DisbursementId;
            fromDate = Convert.ToDateTime(tbFromDate.Text);
            toDate = Convert.ToDateTime(tbToDate.Text);
            int n = idis.FindQuan(itemCode, depId, fromDate, toDate);
            requestq = idis.FindQuan(itemCode, depId, fromDate, toDate);
            return n;
        }
        protected double FindUnitPrice(string itemCode)
        {
            up = isss.FindUnitPrice(itemCode);
            if (up != 0)
                return up;
            else
                return 0;
        }
        protected double FindCost()
        {
            try
            {
                cost = requestq * up;
                requestq = 0;
                up = 0;
                return cost;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('No Record Found !!')</script>");
                return 0;
            }
        }
        protected void Bind()
        {
            if (tbFromDate.Text.Any() && tbToDate.Text.Any())
            {
                depId = ids.SearchDepartmentByName(ddlDepartment.SelectedValue).DeptId;
                fromDate = Convert.ToDateTime(tbFromDate.Text);
                toDate = Convert.ToDateTime(tbToDate.Text);
                dd = idis.GetDisbursementStationeryByDeptId_Date(depId, fromDate, toDate);
                if (dd != null)
                {
                    gvChargeBack.DataSource = idis.GetDisbursementStationeryByDeptId_Date(depId, fromDate, toDate);
                    gvChargeBack.DataBind();
                }
                else
                {
                    gvChargeBack.DataSource = null;
                    gvChargeBack.DataBind();
                    Response.Write("<script>alert('No Record Found !!')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please Select Valid Data to Generate Report !!')</script>");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Bind();
        }

        protected void PDF_Click(object sender, EventArgs e)
        {
            try
            {
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvChargeBack.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                HTMLWorker htmlparser = new HTMLWorker(pdfdoc);
                PdfWriter.GetInstance(pdfdoc, Response.OutputStream);

                pdfdoc.Open();
                htmlparser.Parse(sr);
                pdfdoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachement;filename=ChargeBack.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfdoc);
                //Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Select the required Fields !!')</script>");
            }
        }

        protected void SendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                deptHead = ies.SearchEmployeeByEmpId(ids.SearchDepartmentByName(ddlDepartment.SelectedValue).HeadId);
                int value = idis.UpdateDisbursementStatus(depId, fromDate, toDate);
                if (value == 1)
                {
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter ht = new HtmlTextWriter(sw);
                    gvChargeBack.RenderControl(ht);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.Credentials = new System.Net.NetworkCredential("adteam10superman@gmail.com", "!Passw0rd");
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    MailMessage mm = new MailMessage("adteam10superman@gmail.com", deptHead.Email);
                    mm.Subject = "Charge Back Details";
                    mm.Body = "<h1>Charge Back Details for the period " + fromDate.Date + " to " + toDate.Date + "</h1><br/>" + sw.ToString();
                    mm.IsBodyHtml = true;
                    client.Send(mm);
                    Response.Write("<script>alert('Charge Back status is updated and email is sent to "
                        + deptHead.EmployeeName + " of " + ddlDepartment.SelectedValue + " !!')</script>");

                    //Refresh page
                    gvChargeBack.DataSource = null;
                    gvChargeBack.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('No disbursement of stationeries pending to be charged for this department during this particular duration !!')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Enter a valid Email Id !!')</script>");
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEmail.Text = ies.SearchEmployeeByEmpId(ids.SearchDepartmentByName(ddlDepartment.SelectedValue).HeadId).Email;
        }
    }
}