/* Authors: Lee Kai Seng, Geraldine Tay (UI) */

using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10
{
    public partial class Default1 : System.Web.UI.Page
    {
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Employee emp = authMgmt.SearchEmployeeByUserName(Context.User.Identity.Name);

                int empId = (int)Session["employeeId"];
                empId = emp.EmployeeId;
                Session["employeeId"] = empId;

                string deptId = (string)Session["deptId"];
                deptId = emp.Department.DeptId;
                Session["deptId"] = deptId;

                string path = "~";

                if (Context.User.IsInRole("Department Staff"))
                    path = "~/Dept/ViewStationeryCatalogue.aspx";

                else if (Context.User.IsInRole("Department Head"))
                    path = "~/Dept/RequisitionApprovalPage.aspx";

                else if (Context.User.IsInRole("Store Clerk"))
                    path = "~/Store/LowStock.aspx";

                else if (Context.User.IsInRole("Store Supervisor"))
                    path = "~/Store/AdjustmentRequestsListSup.aspx";

                else if (Context.User.IsInRole("Store Manager"))
                    path = "~/Store/AdjustmentRequestsManager.aspx";

                    Response.Redirect(path);
            }
        }
    }
}