/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using ADProject_Team10.BizLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    public partial class Department_CollectionPointList : System.Web.UI.Page
    {
        IEmployeeService ies = new EmployeeService();
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                lblEemail.Text = Context.User.Identity.Name;
            }
        }

        protected string FindEmployeeName(int empId)
        {
            return ies.SearchEmployeeByEmpId(empId).EmployeeName;
        }
    }
}