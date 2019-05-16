/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10.Models;
using ADProject_Team10.BizLogic;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    public partial class ModifyCollectionPoint : System.Web.UI.Page
    {
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();
        ICollectionPointService icps = new CollectionPointService();
        IEmployeeService ies = new EmployeeService();
        IDepartmentService ids = new DepartmentService();
        string deptId;
        string locationId;
        int employeeId;
        Department department;
        string currentLocation;
        Employee loggedInEmployee;
        private static string upLoc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(authMgmt.IsDeptRep(Context.User.Identity.Name) || Context.User.IsInRole("Department Head")))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                lblAttention.Text = "Attention:" + "<br />" +
                    "Collection is done every Monday, unless informed otherwise.​";

                employeeId = (int)Session["employeeId"];
                loggedInEmployee = ies.SearchEmployeeByEmpId(employeeId);
                deptId = loggedInEmployee.DeptId;
                department = ids.SearchDepartmentByDeptId(loggedInEmployee.DeptId);
                CollectionPoint cp = icps.GetCollectionPointById(department.LocationId);
                Employee storeClerk = ies.SearchEmployeeByEmpId(cp.EmployeeId);
                locationId = department.LocationId;
                lblStoreClerkName.Text = storeClerk.EmployeeName;
                lblContactNumber.Text = storeClerk.Phone;
                lbltime.Text = DateTime.Today.Add(cp.Time).ToString("hh:mm tt");
                lblCurrentLocation.Text = cp.LocationName;
                currentLocation = cp.LocationName;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CollectionPoint currentloc = icps.GetCollectionPointById(department.LocationId);
                String clerkemail = ies.SearchEmployeeByEmpId(currentloc.EmployeeId).Email;
                string headname = ies.SearchEmployeeByEmpId(employeeId).EmployeeName;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("adteam10superman@gmail.com", "!Passw0rd");
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage mm = new MailMessage("adteam10superman@gmail.com", clerkemail);
                mm.Subject = "Change of Collection Point";
                if (upLoc != "")
                {
                    CollectionPoint colp = icps.GetCollectionPointById(upLoc);
                    string locname = colp.LocationName;
                    mm.Body = "Department:\t" + department.DeptName + "\nDepartment Head:\t" + headname
                        + "\nHas changed the location to\t" + locname;
                    upLoc = "";
                    Response.Write("<script>alert('Mail has been sent to " + clerkemail + " !!')</script>");
                }
                else
                {
                    mm.Body = "Department:\t" + department.DeptName + "\nDepartment Head:\t" + headname
                        + "\nMail from Department head highlighting the note that the location is not changed";
                    Response.Write("<script>alert('Mail has been sent to " + clerkemail + " !!')</script>");
                }
                client.Send(mm);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Enter a valid gmail id !!')</script>");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            currentLocation = icps.GetCollectionPointById(department.LocationId).LocationName;
            if (rblCollectionPoint.SelectedItem != null)
            {
                if (rblCollectionPoint.SelectedValue != currentLocation)
                {
                    upLoc = locationId;
                    int i = ids.UpdateCollectionPoint(locationId, deptId);
                    if (i == 1)
                    {
                        Response.Write("<script>alert('Location Updated !!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Cannot Change Location !!')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Chosed the same Location !!')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please Select a particular Collection Point')</script>");
            }
        }

        protected void rblCollectionPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            CollectionPoint cp = icps.GetCollectionPointByName(rblCollectionPoint.SelectedItem.Text);
            Employee emp = ies.SearchEmployeeByEmpId(cp.EmployeeId);
            locationId = cp.LocationId;
            Employee employee = ies.SearchEmployeeByEmpId(employeeId);
            deptId = employee.DeptId;
            lblStoreClerkName.Text = emp.EmployeeName;
            lblContactNumber.Text = emp.Phone;
            lbltime.Text = DateTime.Today.Add(cp.Time).ToString("hh:mm tt");
            lblCurrentLocation.Text = cp.LocationName;
        }
    }
}