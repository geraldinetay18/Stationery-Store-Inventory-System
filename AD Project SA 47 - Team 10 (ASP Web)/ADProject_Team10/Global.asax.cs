using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using ADProject_Team10.Models;

namespace ADProject_Team10
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Session_Start(object sender, EventArgs e)
        {
            Session["employeeId"] = 0;
            Session["deptId"] = "";
            Session["Requests"] = new Requisition();
            Session["RequestsDetails"] = new List<RequisitionDetail>();
            Session["requisitionEmployeeId"] = 0;
        }
    }
}